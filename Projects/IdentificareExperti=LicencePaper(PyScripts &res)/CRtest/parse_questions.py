import sys
import os
import re

def write_data(data, file):
    g = open(file, "ab")
    g.write(data)
    g.close()

f = open(sys.argv[1], "rb")
tagStarted = False
category = ""

data = ""
proc = 0
for line in f:
    #print "#%s"%line
    if tagStarted == True:
        data += line
    if line.startswith("<vespaadd>"):
        if tagStarted == True:
            print "Eroare!!!!"
            sys.exit(0)
        else:
            #print "am gasit inceput de tag"
            tagStarted = True
            data += line
            proc += 1
    else:
        if line.startswith("<cat>"):
            if tagStarted == False:
                print "eroare la categorie!!!!\n%s\n%s"%(data, proc)
                sys.exit(0)
            else:
                regex = re.findall("<cat>(.+)</cat>", line)
                category = regex[0]
        else:
            if line.startswith("</vespaadd>"):
                if tagStarted == False:
                    print "eroare la sfarsitul tagului"
                    sys.exit(0)
                else:
                    tagStarted = False
                    write_data(data, category)
                    data = ""