import sys
import os
import re
import codecs
import xml.etree.ElementTree
#import simplejson
import urllib
import htmlentitydefs

rating = {}
rating2 = {}
association = {}
rez=""

def sortDict(dict, question_id):
    global rez
    list = [(k, v) for (k, v) in dict.iteritems()]
    list.sort(lambda y, x: cmp(x[1], y[1]))
    sum = 0
    dim = 10
    if len(list) < 10:
        dim = len(list)
    for i in range(dim):
        sum += int(list[i][1])
    for i in range(dim):
        rez+= ("%010d Q2 %010d %s %s Exp1\n")%(int(question_id), int(list[i][0].replace("u", "")), i, float(list[i][1]) / sum)
        
def splitTokens(text):
    toate = []
    processed = []
    toate = re.split(" |[+=.,!?*/]|\(|\)|\'|\"",text)
    for element in toate:
        if element != "":
            processed.append(element.lower())
    return processed

def processAnswers(element):
    
    element = element.replace("\n", " ")
    #xprint "Trying to process:\n%s", element, "..."
    text = ""
    try:
        res = re.findall("<subject>(.+)</subject>", element)
        text = res[0]
        #print "has subject"
    except:
        pass
    try:
        res = re.findall("<content>(.+)</content>", element)
        text += " " + res[0]
        #print "has content"
    except:
        pass
    try:
        res = re.findall("<bestanswer>(.+)</bestanswer>", element)
        text += " " + res[0]
        #print "has bestanswer"
    except:
        pass
    return splitTokens(text)

def computeUniqueScore(topicTokens, elementTokens, userID):
    global rating
    #print "&&&&", topicTokens
    #print "####", elementTokens
    for token in topicTokens:
        if token in elementTokens:
            if rating.has_key(userID):
                rating[userID] += 1
            else:
                rating[userID] = 1

def computeFrequencyScore(topicTokens, elementTokens, userID):
    global rating2
    for token in topicTokens:
        for token2 in elementTokens:
            if token == token2:
                if rating2.has_key(userID):
                    rating2[userID] += 2
                else:
                    rating2[userID] = 2
def computeOtherScore(topicTokens, elementTokens, userID, language):
    translated = []
    languages = ["en", "de", "es", "fr"]
    print "try to remove language %s from languages %s"%(language, languages)
    languages.remove(language)
    
    for limba in languages:
        translated = []
        for element in topicTokens:
            translated.append(translate(element, language, limba))
        for token in translated:
            for token2 in elementTokens:
                if token == token2:
                    if rating2.has_key(userID):
                        rating2[userID] += 1
                    else:
                        rating2[userID] = 1
        
            
def processCategory(name, tokens, lang):
        g = open(name, "rb")
        tagStarted = False
        data = ""
        proc = 0
        best_id = -1
        for line in g:
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
                if line.startswith("<best_id>"):
                        if tagStarted == False:
                            print "eroare la sfarsitul tagului"
                            sys.exit(0)
                        else:
                            best_id = re.findall("<best_id>(.+)</best_id>", line)[0]
                else:        
                    if line.startswith("</vespaadd>"):
                        if tagStarted == False:
                            print "eroare la sfarsitul tagului"
                            sys.exit(0)
                        else:
                            tagStarted = False
                            categoryTokens = processAnswers(data)
                            #computeUniqueScore(tokens, categoryTokens, best_id)
                            computeFrequencyScore(tokens, categoryTokens, best_id)
                            #computeOtherScore(tokens, categoryTokens, best_id, lang[0])
                            data = ""
        g.close()
        
def parseTopics():    
    global rating2
    f = open(sys.argv[1], "rb")
    tagStarted = False
    category = ""
    lang = ""
    data = ""
    proc = 0
    allCat = []
    tokens = []
    questionId = 0
    for line in f:
        #print "#%s"%line
        #if proc > 1:
        #    sys.exit(0)
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
                #print proc
        else:
            if line.startswith("</topic>"):
                if tagStarted == False:
                    print "eroare la sfarsitul tagului"
                    sys.exit(0)
                else:
                    tagStarted = False
                    #print tokens
                    processCategory(category, tokens, lang)
                    #sortDict(rating)
                    sortDict(rating2, questionId)
                    #rating = {}
                    rating2 = {}
                    data = ""
            else:
                if line.startswith("<category>"):
                    res = re.findall("<category>(.+)</category>", line)
                    category = res[0]
                else:
                    if line.startswith("<tokens>"):
                        token = re.findall("<tokens>(.+)</tokens>", line)
                        #print token
                        token_aux = []
                        if token != []:
                            #print token[0].split(',')
                            token_aux = token[0].split(',')
                            token_aux = token_aux[0:-1]
                            tokens.extend(token_aux)
                            #print token_aux
                            
                    if line.startswith("<synonyms>"):
                        token = re.findall("<synonyms>(.+)</synonyms>", line)
                        #print token
                        token_aux = []
                        if token != []:
                            token_aux = token[0].split(',')
                            token_aux = token_aux[0:-1]
                            tokens.extend(token_aux)
                            #print token_aux
                    
                    else:
                        if line.startswith("<identifier>"):
                            if tagStarted == False:
                                print "eroare la sfarsitul tagului"
                                sys.exit(0)
                            else:
                                questionId = re.findall("<identifier>(.+)</identifier>", line)[0]
    ff=open("rezTAS","ab")
    ff.write(rez)
    ff.close()

def buildCatgoriesAssociation(filename):
    global association
    f = open(filename, "rb")
    lista = []

    for line in f:
        lista = line.split(",")
        asoc = []
        for element in lista:
            asoc.append(element.strip())
        association[asoc[-1]] = asoc[:-1]
    for v, k in association.iteritems():
        print v, "-->", k
    f.close()

baseUrl = "http://ajax.googleapis.com/ajax/services/language/translate"

def getSplits(text,splitLength=4500):
    '''
    Translate Api has a limit on length of text(4500 characters) that can be translated at once, 
    '''
    return (text[index:index+splitLength] for index in xrange(0,len(text),splitLength))


def translate(text,src='', to='en'):
    '''
    A Python Wrapper for Google AJAX Language API:
    * Uses Google Language Detection, in cases source language is not provided with the source text
    * Splits up text if it's longer then 4500 characters, as a limit put up by the API
    '''

    params = ({'langpair': '%s|%s' % (src, to), 'v': '1.0'})
    retText=''
    for text in getSplits(text):
            params['q'] = text
            resp = simplejson.load(urllib.urlopen('%s' % (baseUrl), data = urllib.urlencode(params)))
            try:
                    retText += resp['responseData']['translatedText']
            except:
                    raise
    return retText.encode("iso-8859-1")


if __name__ == '__main__':
    #buildCatgoriesAssociation("CategoriesAssociation.txt")
    parseTopics()
    
    