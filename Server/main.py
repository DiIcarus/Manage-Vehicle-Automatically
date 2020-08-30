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
from resource.register_vehicle import ApiRegisterVehicle
from resource.request_send_code import ApiRequestSendCode
from resource.send_code import SendCode
from resource.check_out_type_input import ApiCheckOutTypeInput
from resource.check_in import ApiCheckInInsertData
from resource.check_out_with_bot import ApiCheckOutWithBot
from resource.check_in_with_bot import ApiCheckInWithBot
from resource.user.user import ApiInfoUser
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
  global mail
  class HelloWord(Resource):
    # @jwt_required
    def get(self):
      x = {
        "name": [
          "aaa",
          "sss",
          "vvv"
        ],
        "age": 30,
        "city": "New York"
      }
      return x
      # return json2xml.Json2xml(readfromstring('{"Hello":"world"}')).to_xml()
  class SendGmail(Resource):
    def post(self):
      x = {
        "name": [
          "aaa",
          "sss",
          "vvv"
        ],
        "age": 30,
        "city": "New York"
      }
      return x
  
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
  
  # demo
  api.add_resource(HelloWord,'/',)
  api.add_resource(SendGmail,'/gmail')
  api.add_resource(Demo,'/testme')
  # web app
  api.add_resource(ApiRegister,'/register')
  api.add_resource(ApiSignIn,'/login')
  api.add_resource(ApiRegisterMonthTicket,'/register-ticket')
  api.add_resource(ApiRegisterVehicle,"/register-vehicle")
  #
  api.add_resource(ApiRequestSendCode,'/request-send-code') # gen send code
  api.add_resource(SendCode,'/send-code') # owner mail
  # maybe need
  api.add_resource(ApiGetInfoUser,'/info/<string:id_user>')
  # vehicle
  # check in
  api.add_resource(ApiCheckInInsertData,'/check-in-insert-data')
  api.add_resource(ApiCheckInWithBot,'/check-in-with-bot',
  resource_class_kwargs={ 'gmail': mail,"app":app })
  #check out
  api.add_resource(ApiCheckOutTypeInput,'/check-out-insert-data')
  api.add_resource(ApiCheckOutWithBot,'/check-out-with-bot',
  resource_class_kwargs={ 'gmail': mail,"app":app })
  api.add_resource(ApiCheckOutVehicleID,'/check-out-vehicle')
  # manager
  api.add_resource(ApiInfoUser,'/user/users')
  # api.add_resource(ApiInfoCheckIn,'/user/check-in')
  # api.add_resource(ApiInfoCheckOut,'/user/check-out')
  # api.add_resource(ApiVehicle,'/user/vehicle')
  
def main():
  global app
  global api
  global mail
  app = Flask(__name__)
  CORS(app,resources={r"/*": {"origins": "*"}})
  api = Api(app)
  app.config.update(mail_setting)
  mail = Mail(app)
  jwt = JWTManager(app)
  app.config['JWT_SECRET_KEY'] = 'JWT_SECRET_KEY'
  api_add_resource()

  app.run(port=5000)
  
if __name__=="__main__":
  main()
