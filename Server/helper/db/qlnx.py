from uuid import uuid4
from helper.db import dbhelper as db
def createArrayFromReturnCursor(cursor):
  arr = []
  for row in cursor:
    arr.append(row)
  cursor.close()
  return arr

def selectTable(querystr=""):
  '''
    return array(column1,column2, . . )
  '''
  cnx=db.connect()
  result = db.select(
    cnx=cnx,
    querystr=querystr
  )
  result = createArrayFromReturnCursor(result)
  db.disconnect(cnx)
  return result

def insertCheckIn(vehicle_id="",key_code="",dates=""):
  querystr = (
    "INSERT INTO `check-in` "
    "(vehicle_id, key_code, dates)"
    "values(%s,%s,%s)"
  )
  data = (vehicle_id,key_code,dates)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr=querystr,
    data=data
  )
  db.disconnect(cnx)

def insertUser(id_users="", gmail="", phone_number="", dob="", password="", name=""):
  querystr = (
    "INSERT INTO users"
    "(id_users, gmail, phone_number, dob, password, name)"
    "values(%s, %s, %s, %s, %s, %s)"
  )
  data = (id_users, gmail, phone_number, dob, password, name)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr=querystr,
    data=data
  )
  db.disconnect(cnx)
  
def insertOwner(id_owners="", user_id="", private_code="", public_code=""):
  querystr = (
    "INSERT INTO Owners"
    "(id_owners, user_id, private_code, public_code)"
    "values(%s, %s, %s, %s)"
  )
  data = (id_owners, user_id, private_code, public_code)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr=querystr,
    data=data
  )
  db.disconnect(cnx)

def insertCheckOut(vehicle_id="", key_code="", share_code="", dates=""):
  querystr = (
    "INSERT INTO `check-out`"
    "(vehicle_id, key_code, share_code, dates)"
    "values(%s, %s, %s, %s)"
  )
  data = (vehicle_id, key_code, share_code, dates)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr=querystr,
    data=data
  )
  db.disconnect(cnx)
def insertSharingCounter(send_code="",owner_id="", name=""):
  querystr = (
    "INSERT INTO sharing_counter"
    "(send_code,owner_id, name)"
    "values(%s, %s,%s)"
  )
  data = (send_code,owner_id, name)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr=querystr,
    data=data
  )
  db.disconnect(cnx)

def insertVehiclesSharingCounter(id_vehicles="", send_code="",end_date=0,status=0):
  querystr = (
    "INSERT INTO vehicles_sharing_counter"
    "(id_vehicles, send_code,end_date,status)"
    "values(%s,%s,%s,%s)"
  )
  data = (id_vehicles,send_code,end_date,status )
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr=querystr,
    data=data
  )
  db.disconnect(cnx)

def insertVehicle(id_vehicles="",id_owner=""):
  querystr = (
    "INSERT INTO vehicles"
    "(id_vehicles, id_owner)"
    "values(%s,%s)"
  )
  data = (id_vehicles, id_owner)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr=querystr,
    data=data
  )
  db.disconnect(cnx)

def insertTicket(vehicle_id="",date="",duration=0):
  querystr = (
    "INSERT INTO tickets"
    "(vehicle_id, date,duration)"
    "values(%s,%s,%s)"
  )
  data = (vehicle_id, date,duration)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr= querystr,
    data=data
  )
  db.disconnect(cnx)
def updateSendCode(vehicle_id="",send_code="",status=1):
  querystr = (
    "UPDATE vehicles_sharing_counter "
    "SET status="+str(status)+" "
    "WHERE id_vehicles=\'"+vehicle_id+"\' AND send_code=\'"+send_code+"\'"
  )
  data = (status)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr= querystr,
    data=data
  )
  db.disconnect(cnx)
def Register(dob,name,gmail,phone_number,password):
  id_users = str(uuid4()).split('-')[0]
  id_owners = str(uuid4()).split('-')[0]
  private_code = str(uuid4()).split('-')[0]
  public_code = str(uuid4()).split('-')[0]
  try:
    insertUser(
      id_users=id_users,
      gmail=gmail,
      phone_number=phone_number,
      dob=dob,
      password=password,
      name=name
    )
    insertOwner(
      id_owners=id_owners,
      user_id=id_users,
      private_code=private_code,
      public_code=public_code
    )
    return True
  except:
    return False
    
def RegisterVehicle(owner_id,vehicle_id):
  insertVehicle(
    id_vehicles=vehicle_id,
    id_owner=owner_id,
  )
def GenSenCode(share_code,vehicle_id):
  insertSharingCounter(send_code=share_code,owner_id="",name="unknown")
  insertVehiclesSharingCounter(id_vehicles=vehicle_id,send_code=share_code,end_date=1000*60*5,status=0)