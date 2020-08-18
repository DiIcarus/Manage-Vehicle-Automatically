import sys, os
sys.path.append(os.path.abspath('.'))

from flask import Flask
from flask_cors import CORS
from flask_restful import Resource,Api, request
from flask_mail import Mail, Message
from flask_jwt_extended import JWTManager
import json
from json2xml import json2xml
from json2xml.utils import readfromurl, readfromstring, readfromjson
from helper.img import bit642NumpyImg
import cv2

from resource.register import ApiRegister
mail_setting = {
  "MAIL_SERVER": 'smtp.gmail.com',
  "MAIL_PORT": 465,
  "MAIL_USE_TLS": False,
  "MAIL_USE_SSL": True,
  "MAIL_USERNAME": 'nguyenlehaison4074@gmail.com',
  "MAIL_PASSWORD": 'Di Icarus'
}


def api_add_resource():
  global api
  class HelloWord(Resource):
    def get(self):
      x = {
        "name": "John",
        "age": 30,
        "city": "New York"
      }
      return x
      # return json2xml.Json2xml(readfromstring('{"Hello":"world"}')).to_xml()
  class SendGmail(Resource):
    def get(self,email):
      global mail
      global app
      message = Message(
        subject="",
        sender=app.config.get("MAIL_USERNAME"),
        recipients=[email],
        body="Flask QLNX send mail 1234"
      )
      mail.send(message)
      return "mail is send"
  
  class Demo(Resource):
    def post(self):
      byte = request.get_data().decode('utf-8')
      json_demo = json.loads(byte)
      # print(json_demo["base64"])
      cv2.imshow("demo",bit642NumpyImg(json_demo["base64"]))
      cv2.waitKey(0)
      #using for c#x

    def get(self):
      return {"test":"tetss"}

  api.add_resource(HelloWord,'/',)
  api.add_resource(SendGmail,'/<string:email>')
  api.add_resource(ApiRegister,'/register')
  api.add_resource(Demo,'/testme')
  
def main():
  global app
  global api
  global mail
  app = Flask(__name__)
  CORS(app,resources={r"/*": {"origins": "*"}})
  api = Api(app)
  api_add_resource()
  app.config.update(mail_setting)

  app.config['JWT_SECRET_KEY'] = 'JWT_SECRET_KEY'

  jwt = JWTManager(app)
  mail = Mail(app)
  app.run(port=5000)
  
if __name__=="__main__":
  main()