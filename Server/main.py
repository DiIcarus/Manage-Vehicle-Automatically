import sys, os
sys.path.append(os.path.abspath('.'))

from flask import Flask
from flask_cors import CORS
from flask_restful import Resource,Api, request
from flask_mail import Mail, Message
from flask_jwt_extended import JWTManager,jwt_required
import json
from json2xml import json2xml
from json2xml.utils import readfromurl, readfromstring, readfromjson
from helper.img import bit642NumpyImg
import cv2
import numpy as np

import resource
from resource.check_out import ApiCheckOutVehicleID, ApiCheckOutBotSendCodeMail,ApiCheckoutIncomeKey
from resource.sign_in import ApiSignIn, ApiGetInfoUser
from resource.register import ApiRegister
from resource.register_month_ticket import ApiRegisterMonthTicket
from lib.detector import detect_character
from lib.recognition import identify_character

from lib.predict_class import NeuralNetwork
import tensorflow as tf
from tensorflow.python.keras.backend import set_session
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
    @jwt_required
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

      img=bit642NumpyImg(json_demo["base64"])
      # cv2.imwrite("demo.jpg",img)

      # try:
      # im_test = cv2.imread('./Core/demo/vietnam_car_rectangle_plate.jpg')
      # # cv2.imshow("test",img)
      
      # images = detect_character(im_test)
      # result = []
      # for im in images:
      #   result.append(identify_character(im))
      # print(result)
      session = tf.compat.v1.Session()
      graph = tf.compat.v1.get_default_graph()
      set_session(session)
      ai = NeuralNetwork(session,graph)
      try:
        ai.predict(img)
        return{
          "status":200,
          "message":"my sign"
        }
      except:
      # cv2.waitKey(1000)
      # cv2.destroyAllWindows()
        return {
          "asd":"kajsdhf",
          "asdaaa":123
        }
      # except:
      #   return {
      #     "asd":"none",
      #     "asdaaa":123
      #   }

    def get(self):
      return {"test":"tetss"}

  api.add_resource(HelloWord,'/',)
  api.add_resource(SendGmail,'/<string:email>')
  api.add_resource(Demo,'/testme')
  #demo
  api.add_resource(ApiRegister,'/register')
  api.add_resource(ApiSignIn,'/login')
  api.add_resource(ApiGetInfoUser,'/info/<string:id_user>')
  api.add_resource(ApiRegisterMonthTicket,'/register-ticket')
  api.add_resource(ApiCheckOutVehicleID,'/check-out-vehicle')
  api.add_resource(ApiCheckOutBotSendCodeMail,'/check-out-bot-send-mail')
  api.add_resource(ApiCheckoutIncomeKey,'/check-out-by-key')
  
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
  # im_test = cv2.imread('./Core/demo/vietnam_car_rectangle_plate.jpg')
  # # cv2.imshow("test",img)
  
  # images = detect_character(im_test)
  # result = []
  # for im in images:
  #   result.append(identify_character(im))
  # print(result)
