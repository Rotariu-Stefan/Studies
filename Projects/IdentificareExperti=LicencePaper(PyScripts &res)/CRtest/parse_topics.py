import sys
import os
import re
import codecs
import xml.etree.ElementTree


def write_data(data, file):
    g = open(file, "ab")
    g.write(data)
    g.close()
def splitTokens(text, lang):
    toate = []
    stopWords = []
    good = []
    toate = re.split(" |[+=.,!?*/-]|\(|\)|\'|\"",text)
    print lang[0]
    h = open(lang[0], "rb")
    line = ""
    for line in h:
        stopWords.append(line.strip())
    for element in toate:
        element = element.lower()
        if element != "" and (element not in stopWords):
            good.append(element)
    print good
    return good

def process_tag(data, lang):
    title = re.findall("<title>(.+)</title>", data)
    description = re.findall("<description>(.+)</description", data)
    text = title[0]
    try:
        text += " " + description[0]
    except:
        pass
    #print text
    tokens = splitTokens(text, lang)
    text = ""
    for element in tokens:
        text += element + ","
    data = data.replace("</topic>", "<tokens>%s</tokens>\n</topic>"%text)
    return data

f = open(sys.argv[1], "rb")
tagStarted = False
category = ""
lang = ""
data = ""
proc = 0
for line in f:
    #print "#%s"%line
    if tagStarted == True:
        data += line
    if line.startswith("<topic "):
        if tagStarted == True:
            print "Eroare!!!!"
            print line, "...", data
            sys.exit(0)
        else:
            #print "am gasit inceput de tag"
            tagStarted = True
            data += line
            lang = re.findall('<topic lang="(.+)">', line)
            proc += 1
            print proc
    else:
        if line.startswith("</topic>"):
            if tagStarted == False:
                print "eroare la sfarsitul tagului"
                sys.exit(0)
            else:
                tagStarted = False
                data = process_tag(data, lang)
                write_data(data, "topic2.xml")
                data = ""