import datetime
import time
def convertString2Timestamp(time_string):
  return time.mktime(datetime.datetime.strptime(time_string,"%d/%m/%Y").timetuple())
  
def getTimeStameNow():
  return time.mktime(datetime.datetime.now().timetuple())