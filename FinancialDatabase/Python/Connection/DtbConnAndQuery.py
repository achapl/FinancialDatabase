import mysql.connector
import sys


errorCount = 0

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


def search():
	return

def runQuery(query):
	#print(query)
	# user: testuser, pass: testuser
	cnx = mysql.connector.connect(user='testuser', password='testuser', host='127.0.0.1', database='tool_database')
	cnx.autocommit=True
	cursor = cnx.cursor()
	cnx.rollback()
	result = None
	try:
		result = cursor.execute(query) ## TRY: Finding source code for commit(), restarting a database connection project, creating new database user
		result = [result, cursor.fetchall()]
		try:
			cnx.commit()
		except mysql.connector.errors.InternalError:
			print("Error: Unread Result")
		retStr = ""
		if result is not None:
			retStr = str(result)
		cursor.close()
		cnx.close()
	except:
		global errorCount
		errorCount += 1
		print("!!!ERROR!!!", file=sys.stderr)
		print(query, file=sys.stderr)
		print("", file=sys.stdout)
		return ["ERROR","ERROR"], [-1]
	return result, cursor.lastrowid


result, empty = runQuery(getQuery())

if (result[1] != None):
	if result[1] == "ERROR":
		print(result[1])
	else:
		for row in result[1]:
			print(row)
print("EOS")

#cursor.close()

#cnx.close()