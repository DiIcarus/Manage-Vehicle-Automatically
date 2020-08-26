
import cv2
import numpy as np
from lib.local_utils import detect_lp
from os.path import splitext, basename
from keras.models import model_from_json
import glob
import tensorflow as tf


IMAGE_PATHS = glob.glob("./Core/demo/*.jpg")
WPOD_NET_PATH = "./Core/model/wpod-net.json"

def load_model():
    path = WPOD_NET_PATH
    try:
        path = splitext(path)[0]
        with open('%s.json' % path, 'r') as json_file:
            model_json = json_file.read()
        model = model_from_json(model_json, custom_objects={})
        model.load_weights('%s.h5' % path)
        print("Loading model successfully...")
        model._make_predict_function()
        return model,tf.get_default_graph()
    except Exception as e:
        print(e)

# WPOD_NET_MODEL = load_model()

def preprocess_input_image(np_image,resize=False):
    img = cv2.cvtColor(np_image, cv2.COLOR_BGR2RGB)
    img = img / 255
    if resize:
        img = cv2.resize(img, (224,224))
    return img

def get_plate(model,np_image,graph, Dmax=608, Dmin=256):
  with graph.as_default():
    vehicle_image = preprocess_input_image(np_image)
    ratio = float(max(vehicle_image.shape[:2])) / min(vehicle_image.shape[:2])
    side = int(ratio * Dmin)
    bound_dim = min(side, Dmax)
    _ , LpImg, _, cor,graph_detect = detect_lp(model,graph, vehicle_image, bound_dim, lp_threshold=0.5)
    return LpImg, cor,graph_detect

def draw_border_plate(np_image, cor, thickness=3): 
    pts=[]  
    x_coordinates=cor[0][0]
    y_coordinates=cor[0][1]
    for i in range(4):
        pts.append([int(x_coordinates[i]),int(y_coordinates[i])])

    pts = np.array(pts, np.int32)
    pts = pts.reshape((-1,1,2))

    vehicle_image = preprocess_input_image(np_image)
    
    cv2.polylines(vehicle_image,[pts],True,(0,255,0),thickness)
    return vehicle_image

def make_binary_image(image):
  gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
  blur = cv2.GaussianBlur(gray,(7,7),0)
  binary = cv2.threshold(blur, 180, 255,
    cv2.THRESH_BINARY_INV + cv2.THRESH_OTSU)[1]
  return binary

def get_images_character(cont,test_roi,thre_mor,graph):

  def sort_contours(cnts,reverse = False):
    i = 1
    boundingBoxes = [cv2.boundingRect(c) for c in cnts]
    (cnts, boundingBoxes) = zip(*sorted(zip(cnts, boundingBoxes),
                      key=lambda b: b[1][i], reverse=reverse))
    # print(cnts)
    # filter
    count = 0
    cnt_rect_arr = []
    filter_arr = []
    average_h = 0;
    for c in cnts:
      br = cv2.boundingRect(c)
      # print(br)
      cnt_rect_arr.append(br)
    #sort byy y
    def takeSecond(elem):
      return elem[3]
    cnt_rect_arr.sort(key=takeSecond)
    # print("aa",cnt_rect_arr)
    #filter by height
    filter_10 = []
    for c in cnt_rect_arr:
      if (c[3]<150 and c[3]>40):
        filter_10.append(c)
    # print("bb",filter_10)
    #sort x,y
    rows1 = []
    rows2= []
    for c in filter_10:
      if(rows1==[]):
        rows1.append(c)
      else:
        if c[1]<(rows1[0][1]+10) and c[1] > (rows1[0][1]-10):
          rows1.append(c)
        elif (rows2==[]):
          rows2.append(c)
        elif (c[1]<rows2[0][1]+10 and c[1] > rows2[0][1]-10):
          rows2.append(c)
    # print("R1",rows1)
    # print("R2",rows2)
    #sort x
    def takeX(elem):
      return elem[0]
    rows1.sort(key=takeX)
    rows2.sort(key=takeX)
    # print("R1s",rows1)
    # print("R2s",rows2)
    # print("Done",rows1+rows2)

    return rows1+rows2
  
  def sort_bounding_rect(cont):
    (x, y, w, h) = cv2.boundingRect(cont)
    print(x, y, w, h)
    '''
      short by x, y, w, h
      pop what unable plate
      increase shape
      retrain model with non plate data
      check data (w, i, 1, 5, s, 7,)
    '''
    return x, y, w, h
  with graph.as_default():
    crop_characters = []
    digit_w, digit_h = 30, 60
    for c in sort_contours(cont):
      # x, y, w, h = sort_bounding_rect(c)
      x,y,w,h = c
      ratio = h/w
      if 1<= ratio <=4.5:
        cv2.rectangle(test_roi, (x, y), (x + w, y + h), (0, 255,0), 2)
        curr_num = thre_mor[y:y+h,x:x+w]
        curr_num = cv2.resize(curr_num, dsize=(digit_w, digit_h))
        _, curr_num = cv2.threshold(curr_num, 220, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU)
        crop_characters.append(curr_num)
    return crop_characters,graph

def get_binary_images_character(LpImg,graph):
  with graph.as_default():
    if (len(LpImg)):  
      plate_image = cv2.convertScaleAbs(LpImg[0], alpha=(255.0))
      binary = make_binary_image(plate_image)
      kernel3 = cv2.getStructuringElement(cv2.MORPH_RECT, (3, 3))
      thre_mor = cv2.morphologyEx(binary, cv2.MORPH_DILATE, kernel3)

      cont, _  = cv2.findContours(binary, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
      test_roi = plate_image.copy()

      crop_characters,crop_graph = get_images_character(cont,test_roi,thre_mor,graph)
      return crop_characters, crop_graph

def detect_character(np_image):
  WPOD_NET_MODEL,graph = load_model()
  LpImg, _,graph_get_plate = get_plate(WPOD_NET_MODEL,np_image,graph)
  return get_binary_images_character(LpImg,graph_get_plate)

def main():
  for i in IMAGE_PATHS:
    img = cv2.imread(i)
    detect_character(img)

if __name__ == "__main__":
  main()