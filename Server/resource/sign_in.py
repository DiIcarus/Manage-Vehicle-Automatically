from flask_restful import Resource,request
from helper.db import qlnx as db
from werkzeug.security import safe_str_cmp
from flask_jwt_extended import (
    create_access_token, 
    create_refresh_token, 
    jwt_required, 
    jwt_refresh_token_required, 
    get_jwt_identity, 
    get_raw_jwt
  )
import json
import re
from datetime import datetime
from helper.utils.time_support import convertString2Timestamp,getTimeStameNow



validate_message={
  "Length_Empty" : 'Wrong Input length',
  "Mail_Input_Format_Wrong" : 'Gmail input format wrong',
  "Length_Long" : "Input length so long",
  "Success":"No error",
  "Gmail_Not_Found":"Gmail not found",
  "Wrong_Password":"Wrong password"
}

class Validate:
  def __init__(self, status, message):
    self.status = status
    self.message = message
class ResponseSignIn:
  def __init__(self,status,message,access_token):
    self.status = status
    self.message = message
    self.access_token = access_token

class ResponceGetInFo:
  def __init__(
    self,
    status,
    name,
    gmail,
    vehicle_ids,
    phone_number,
    id_owner,
    private_code,
    public_code,
    send_code,
    dob,
    password,
    ticket_available
    ):
    self.status=status
    self.name = name
    self.gmail = gmail
    self.vehicle_ids = vehicle_ids
    self.phone_number = phone_number,
    self.id_owner = id_owner
    self.private_code = private_code
    self.public_code = public_code
    self.send_code = send_code
    self.dob = dob
    self.password = password
    self.ticket_available = ticket_available

def  validatInput(gmail, password):
  if len(gmail) <0 and len(password) <0:
    return Validate(status=False,message=validate_message.get("Length_Empty"))
  if len(gmail) >45 and len(password)>10:
    return Validate(status=False,message=validate_message.get("Length_Long"))
  if not re.match(r"^[A-Za-z0-9\.\+_-]+@[A-Za-z0-9\._-]+\.[a-zA-Z]*$", gmail):
    return Validate(status=False,message=validate_message.get("Mail_Input_Format_Wrong"))
  return Validate(status=True,message=validate_message.get("Success"))

def validateData(gmail,password):
  gmail_current = db.selectTable("SELECT gmail FROM USERS WHERE gmail=\'"+gmail+"\'")
  password_current = db.selectTable("SELECT gmail FROM USERS WHERE password=\'"+password+"\'")
  if gmail_current ==[]:
    return Validate(status=False,message=validate_message.get("Gmail_Not_Found"))
  elif password_current == []:
    return Validate(status=False,message=validate_message.get("Wrong_Password"))
  else:
    return Validate(status=True,message=validate_message.get("Success"))

def getInfoUser(id_user):
  id_users,gmail,phone_number,dob,password,name = db.selectTable("SELECT * FROM USERS WHERE id_users=\'"+id_user+"\'")[0]
  print(id_users,gmail,phone_number,dob,password,name)
  id_owners,_,private_code,public_code = db.selectTable("SELECT * FROM OWNERS WHERE user_id=\'"+id_user+"\'")[0]
  vehicle_id,_ = db.selectTable("SELECT * FROM vehicles WHERE id_owner=\'"+id_owners+"\'")[0]
  is_available = db.selectTable("SELECT * FROM tickets WHERE vehicle_id=\'"+vehicle_id+"\'")
  # table_vehicle = db.selectTable("SELECT * FROM VEHICLES")
  # table_owners= db.selectTable("SELECT * FROM Owners")
  # print("VEHICLE(id_vehicle,id_owner)",table_vehicle)
  # print("OWNER(ID_owner,id_user,private,public)",table_owners)

  #check ticket before go to this steps.
  if is_available==[]:
    ticket_available = False
  else:
    ticket_available = True
  return json.dumps(ResponceGetInFo(
    status=200,
    name=name,
    gmail=gmail,
    vehicle_ids=vehicle_id,
    phone_number=phone_number,
    id_owner=id_owners,
    private_code=private_code,
    public_code=public_code,
    send_code="",
    dob=datetime.timestamp(dob),
    password=password,
    ticket_available=ticket_available
  ).__dict__) 
class ApiSignIn(Resource):
  def post(self):
    #
    try:
      byte = request.get_data().decode('utf-8')
      json_demo = json.loads(byte)
      gmail = json_demo["gmail"]
      password = json_demo["password"]
    except:
      gmail = request.form['gmail']
      password = request.form['password']
    #
    info = {
      "id_user":"",
      "user_name":"",
      "vehicle_ids":[],
      "private_code":"",
      "public_code":"",
      "gmail":"",
      "dob":"",
      "password":"",
      "phone_number":"",
      "id_owner":"",
    }
    

    validate = validatInput( gmail=gmail, password=password)
    if validate.status:
      validate = validateData(gmail=gmail,password=password)
      if validate.status:
        #
        info["gmail"] = gmail
        info["password"]=password
        id_users,_,phone_number,dob,_,name = db.selectTable("SELECT * FROM USERS WHERE gmail=\'"+gmail+"\'")[0]
        info["id_user"] = id_users
        info["phone_number"] = phone_number
        info["dob"] = dob
        info["user_name"] = name,
        id_owner,_,private_code,public_code = db.selectTable("SELECT * FROM owners WHERE user_id=\'"+id_users+"\'")[0]
        info["private_code"]=private_code
        info["public_code"] = public_code
        info["id_owner"] = id_owner
        result = db.selectTable("SELECT * FROM vehicles WHERE id_owner=\'"+id_owner+"\'")
        
        arr = []
        for r in result:
          ve_info={
            "id":"",
            "ticket_available":"",
          }
          id_vehicle,_= r
          ve_info["id"] = id_vehicle
          res = db.selectTable("SELECT * FROM tickets WHERE vehicle_id=\'"+id_vehicle+"\'")
          for re in res:
            print(re)
            _,_,date,duration = re
            if(date+duration >getTimeStameNow()):
              ve_info["ticket_available"] = "true"
          if ve_info["ticket_available"] != "true":
            ve_info["ticket_available"]="false"
          arr.append(ve_info)
        info["vehicle_ids"].append(arr)
        access_token = create_access_token(identity = info)#id_user
        #
        return  ResponseSignIn(status=200,message=validate.message,access_token=access_token).__dict__
      else:
        return  ResponseSignIn(status=400,message=validate.message,access_token="").__dict__
    else:
      return  ResponseSignIn(status=400,message=validate.message,access_token="").__dict__

class ApiGetInfoUser(Resource):
  # @jwt_required
  def get(self,id_user):
    print(id_user)
    #get info owner
    return getInfoUser('c814007c')
