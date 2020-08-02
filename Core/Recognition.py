from PIL import Image
import numpy as np
import cv2
from keras.models import load_model

MODEL = load_model('./Core/model/character-model.h5')
PATH_IMAGE = './Core/dataset_characters/H/46378_1.jpg'
CLASSES_LABEL = { 1:'0',
            2:'1', 
            3:'2', 
            4:'3', 
            5:'4', 
            6:'5', 
            7:'6',
            8:'7', 
            9:'8', 
            10:'9', 
            11:'A', 
            12:'B', 
            13:'C', 
            14:'D', 
            15:'E', 
            16:'F', 
            17:'G', 
            18:'H', 
            19:'I', 
            20:'J', 
            21:'K', 
            22:'L', 
            23:'M', 
            24:'N', 
            25:'O', 
            26:'P', 
            27:'Q', 
            28:'R', 
            29:'S', 
            30:'T', 
            31:'U',
            32:'V', 
            33:'W', 
            34:'X', 
            35:'Y', 
            36:'Z', 
        }

def identify_character(image):
    image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    image = Image.fromarray(image).convert('RGB').resize((30,30))
    image = np.expand_dims(image, axis=0)
    image = np.array(image)
    pred = MODEL.predict_classes([image])[0]
    sign = CLASSES_LABEL[pred+1]
    return sign

def main():
    img = cv2.imread(PATH_IMAGE)
    print(identify_character(img))

if __name__ == "__main__":
    main()