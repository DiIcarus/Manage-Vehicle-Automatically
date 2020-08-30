from flask_restful import Resource,request
import datetime
from helper.db import qlnx as db
from flask_jwt_extended import jwt_required
import json
from helper.utils.time_support import convertString2Timestamp,getTimeStameNow
from helper.img import bit642NumpyImg
import tensorflow as tf
from tensorflow.python.keras.backend import set_session
from flask_mail import Mail, Message
from lib.detector import detect_character
from lib.recognition import identify_character
from lib.predict_class import NeuralNetwork

validate_message={
  "Length_Empty" : 'Wrong Input length',
  "Mail_Input_Format_Wrong" : 'Gmail input format wrong',
  "Length_Long" : "Input length so long",
  "Success":"No error",
  "Vehicle_Not_Found":"Vehicle not found",
  "Wrong_Password":"Wrong password"
}
  # name
  # Email
  # Phone number
  # Ticket Available
class Response:
  def __init__(self,status,message,vehicle_id,name,gmail,phone_number,ticket_available):
    self.status=status
    self.message = message
    self.name = name
    self.gmail = gmail
    self.phone_number = phone_number
    self.ticket_available = ticket_available
    self.vehicle_id = vehicle_id

class Validate:
  def __init__(self, status, message):
    self.status = status
    self.message = message

def validateInput(vehicle_id,share_code):
  def checkInRange(data,fnum,lnum):
    return len(data)<fnum and len(data)>lnum

  if checkInRange(vehicle_id,0,10):
    return Validate(status=False,message="vehicle_id range incorrectly")
  if checkInRange(share_code,0,10):
    return Validate(status=False,message="share_code range incorrectly")
  return Validate(status=True,message="no error")

def validateData(vehicle_id):
  result = db.selectTable(
    "SELECT * FROM VEHICLES WHERE ID_VEHICLES=\'"+vehicle_id+"\'"
  )
  if result == []:
    return Validate(status=False,message=validate_message.get("Vehicle_Not_Found"))
  else:
    return Validate(status=True,message=validate_message.get("Success"))

def selectChekingData():
  table_ticket = db.selectTable(
    "SELECT * FROM Tickets "
  )
  print(table_ticket)
  table_vehicle = db.selectTable(
    "SELECT * FROM VEHICLES "
  )
  print(table_vehicle)

def checkSendCode(key_code,vehicle_id):
  result_send_code = db.selectTable("SELECT send_code,end_date FROM vehicles_sharing_counter WHERE id_vehicles=\'"+vehicle_id+"\' AND status=0")
  if(result_send_code==[]):
    return Validate(status=True,message="Success")
  else:
    return Validate(status=False,message="Key invalid")

def checkKey(share_code,vehicle_id):
  owner_id = db.selectTable("SELECT id_owner FROM vehicles WHERE id_vehicles=\'"+vehicle_id+"\'")[0]
  result = db.selectTable("SELECT private_code,public_code FROM owners WHERE id_owners=\'"+owner_id+"\'")
  if result==[]:
    return Validate(status=False,message="Key invalid")
  else:
    private_code,public_code = result[0]
    if(public_code==share_code):
      validate = checkSendCode(key_code=share_code,vehicle_id=vehicle_id)
      if(private_code==key_code):
        return Validate(status=True,message="Success")
      elif(validate.status):
        return validate
      else:
        return validate
    else:
      return Validate(status=False,message="Key invalid")

def checkTicketAvailable(vehicle_id):
  ticket = db.selectTable(
    "SELECT dates,duration FROM tickets WHERE vehicle_id=\'"+vehicle_id+"\'"
  )
  if ticket == []:
    return Validate(status=False,message="Ticket unavailable")
  else:
    for t in ticket:
      date,duration = t
      if((date+duration)>getTimeStameNow()):
        return Validate(status=True,message="Ticket available")
    return Validate(status=False,message="Ticket unavailable")

class ApiCheckInWithBot(Resource):
  def __init__(self,**kwargs):
    self.mail = kwargs['gmail']
    self.app = kwargs['app']
  # @jwt_required
  def post(self):
    byte = request.get_data().decode('utf-8')
    json_demo = json.loads(byte)
    img=bit642NumpyImg(json_demo["base64"])
    date = getTimeStameNow()
    session = tf.compat.v1.Session()
    graph = tf.compat.v1.get_default_graph()
    set_session(session)
    ai = NeuralNetwork(session,graph)
    try:
      vehicle_id = ai.predict(img)
      check_vehicle_id = db.selectTable("SELECT * FROM vehicles WHERE id_vehicles=\'"+vehicle_id+"\'")
      if check_vehicle_id != []:
        _,id_owner = check_vehicle_id[0]
        ticket_available=check_vehicle_ticket_available(vehicle_id)
        gmail,name,phone_number=db.selectTable("SELECT gmail,name,phone_number FROM users WHERE id_users=(SELECT user_id FROM owners WHERE id_owners=\'"+id_owner+"\')")[0][0]
        if ticket_available:
          message = Message(
            subject="Bot send mail",
            sender=self.app.config.get("MAIL_USERNAME"),
            recipients=[gmail],
            body="Your vehicle just checked in, vehicle status: "+str(ticket_available)
          )
          self.mail.send(message)
          db.insertCheckIn(
          vehicle_id=vehicle_id,
          key_code="",
          dates=getTimeStameNow()
          )
          return Response(
            status=201,
            message="Success",
            name=name,
            gmail=gmail,
            phone_number=phone_number,
            ticket_available=ticket_available,
            vehicle_id=vehicle_id
            ).__dict__
        else:
          message = Message(
            subject="Bot send mail",
            sender=self.app.config.get("MAIL_USERNAME"),
            recipients=[gmail],
            body="Your vehicle just checked in, vehicle status: "+str(ticket_available)
          )
          self.mail.send(message)
          return Response(
            status=201,
            message="Success",
            name=name,
            gmail=gmail,
            phone_number=phone_number,
            ticket_available=ticket_available,
            vehicle_id=vehicle_id,
            ).__dict__
      else:
        return Response(
          status=200,
          message=vehicle_id + " not found, please register !!",
          name="",
          gmail="",
          phone_number="",
          ticket_available="",
          vehicle_id=vehicle_id
          ).__dict__
    except:
      return Response(
        status=400,
        message="Fail",
        name="",
        gmail="",
        phone_number="",
        ticket_available="",
        vehicle_id=""
        ).__dict__


