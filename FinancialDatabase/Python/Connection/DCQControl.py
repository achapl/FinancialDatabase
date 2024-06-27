import DtbConnAndQuery
import sys


def getArgs():
	
	numArgs = len(sys.argv)
	argStr = ""

	for i in range(1,numArgs):
		argStr += sys.argv[i] + " "
	
	return argStr


def formatArgs(argStr):
	escapeChar = "^"
	formatArgString = argStr.replace(escapeChar, '')

	return formatArgString


def getQuery():
	args = getArgs()
	query = formatArgs(args)
	return query

def useArgsToRunQuery():
	result, empty = DtbConnAndQuery.runQuery(getQuery())

	if (result[1] != None):
		if result[1] == "ERROR":
			print(result[1])
		else:
			for row in result[1]:
				print(row)
	print("EOS")

useArgsToRunQuery()