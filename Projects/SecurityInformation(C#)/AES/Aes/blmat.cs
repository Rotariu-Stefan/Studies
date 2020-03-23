using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aes
{
    class blmat
    {
        #region data
        private bloc[] blist;
        private int len;
        private int nBl; //asd

        private byte[,] key = new byte[,]
        {
        {0x2b,0x28,0xab,0x09},
        {0x7e,0xae,0xf7,0xcf},
        {0x15,0xd2,0x15,0x4f},
        {0x16,0xa6,0x88,0x3c}
        };
        private int keyround = 0;
        private byte[][,] keyl=new byte[11][,];
        #endregion

        #region constructori
        public blmat()
        {
            len = 1;
            blist = new bloc[len];
            keyl[0] = key;
        }
        public blmat(byte[] b)
        {
            len = 1;
            blist = new bloc[len];
            blist[0] = new bloc(b);
            keyl[0] = key;
        }
        public blmat(String s)
        {
            int n, j, i = 0;
            n = s.Length / 16;
            String[] str;

            nBl = (16-(s.Length % 16));//asd
            if (s.Length % 16 != 0)
            {
                i = 1;
                str = new String[n+1];
            }
            else
                str = new String[n];

            for (j = 0; j < n; j++)
                str[j] = s.Substring(j*16, 16);
            if (i == 1)
            {
                str[n] = s.Substring(j * 16, s.Length - j * 16) + (new String('\v', (16 - (s.Length - j * 16))));
                len = n + 1;
            }
            else
                len = n;

            blist=new bloc[len];
            for (j = 0; j < len; j++)
                blist[j] = new bloc(str[j]);
            keyl[0] = key;
        }
        #endregion

        #region returns
        public String retblmatstr()
        {
            String st = "";
            String aux = "";//asd
            for (int i = 0; i < len-1; i++)
                st = st + blist[i].retblstr();
            aux = blist[len - 1].retblstr();
            aux = aux.Substring(0, aux.Length - nBl);
            st = st + aux;
            return st;
        }
        public bloc[] retblmat()
        {
            return blist;
        }
        public void afisblmat()
        {
            Console.Write("_____________");
            for (int i = 0; i < len; i++)
                blist[i].afisbl();
            Console.WriteLine("_____________");
        }
        public void afiskey()
        {
            Console.WriteLine();
            for (int l = 0; l <= 3; l++)
            {
                Console.Write("\n|");
                for (int c = 0; c <= 3; c++)
                    Console.Write(String.Format("{0:X}", key[l, c]) + "|");
            }
            Console.WriteLine();
        }
        public void afisrkeylist()
        {
            for (int count = 0; count < 11; count++)
            {
                Console.WriteLine();
                for (int l = 0; l <= 3; l++)
                {
                    Console.Write("\n|");
                    for (int c = 0; c <= 3; c++)
                        Console.Write(String.Format("{0:X}", keyl[count][l, c]) + "|");
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region keystuff
        public void acceptKey(byte[,] k)
        {
            key=k;
            keyl[0] = k;
            for (int i = 1; i < 10; i++)
                keyl[i] = null;
            keyround = 0;
        }
        public byte[,] generateRKey()
        {
            byte[,] kx = new byte[4, 4];
            kx[3, 0] = bloc.SBox[(keyl[keyround][0, 3] / 16), (keyl[keyround][0, 3] % 16)];
            for (int i = 1; i < 4; i++)
                kx[i - 1, 0] = bloc.SBox[(keyl[keyround][i, 3] / 16), (keyl[keyround][i, 3] % 16)];

            for (int i = 0; i < 4; i++)
                kx[i, 0] = (byte)(keyl[keyround][i, 0] ^ kx[i, 0] ^ bloc.Rcon[i, keyround]);
            for (int j = 1; j < 4; j++)
                for (int i = 0; i < 4; i++)
                    kx[i, j] = (byte)(keyl[keyround][i, j] ^ kx[i, j - 1]);
    //        key = kx;
            keyround++;
            return kx;
        }
        public void generateRKeyList()
        {
            for(int i=1;i<11;i++)
                keyl[i]=generateRKey();
            keyround = 0;
        }
        public void addRKey(bloc bl, int cnt)
        {
            for (int c = 0; c < 4; c++)
                for (int l = 0; l < 4; l++)
                    bl.retbl()[l, c] = (byte)(bl.retbl()[l, c] ^ keyl[cnt+1][l, c]);
        }
        #endregion

        #region cicle
        public void roundPass()
        {
            for (int i = 0; i < len; i++)
            {
                blist[i].subBytes();
                afisblmat();
                blist[i].shiftRows();
                afisblmat();
                blist[i].mixColumns();
                afisblmat();
                addRKey(blist[i],keyround);
                afisblmat();
            }
            keyround++;
        }
        public void Crypt()
        {
            afisblmat();
            for (int i = 0; i < 9; i++)
                roundPass();
            for (int i = 0; i < len; i++)
            {
                blist[i].subBytes();
                afisblmat();
                blist[i].shiftRows();
                afisblmat();
                addRKey(blist[i], keyround);
                afisblmat();
            }
        }
        #endregion

        #region inverse cicle

        public void invaddRKey(bloc bl, int cnt)
        {
            for (int c = 0; c < 4; c++)
                for (int l = 0; l < 4; l++)
                    bl.retbl()[l, c] = (byte)(bl.retbl()[l, c] ^ keyl[cnt + 1][l, c]);
        }
        public void invroundPass()
        {
            for (int i = 0; i < len; i++)
            {
                addRKey(blist[i], keyround);
                afisblmat();
                blist[i].invmixColumns();
                afisblmat();
                blist[i].invshiftRows();
                afisblmat();
                blist[i].invsubBytes();
                afisblmat();
            }
            keyround--;
        }
        public void deCrypt()
        {
            for (int i = 0; i < len; i++)
            {
                addRKey(blist[i], keyround);
                afisblmat();
                blist[i].invshiftRows();
                afisblmat();
                blist[i].invsubBytes();
                afisblmat();
            }
            keyround--;
            for (int i = 0; i < 9; i++)
                invroundPass();
            keyround = 0;
        }

        #endregion
    }
}
