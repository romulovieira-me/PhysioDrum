import cvzone
from cvzone.ColorModule import ColorFinder
import cv2
import socket # Permite o envio de informações para o Unity

# Iniciando a webcam
cap = cv2.VideoCapture(0)
cap.set(3, 1280)
cap.set(4, 720)

success, img = cap.read()
h, w, _ = img.shape

# Descobrindo a cor do objeto
myColorFinder = ColorFinder(False)
hsvVals = {'hmin': 155, 'smin': 24, 'vmin': 165, 'hmax': 169, 'smax': 203, 'vmax': 255} #Rosa


sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 5053) # IP e porta da máquina que receberão as informações

while True:
    success, img = cap.read()
    imgColor, mask = myColorFinder.update(img, hsvVals)
    imgContour, contours = cvzone.findContours(img, mask)

    if contours:
        data = contours[0]['center'][0],\
               h-contours[0]['center'][1],\
               int(contours[0]['area'])
        print(data)
        sock.sendto(str.encode(str(data)), serverAddressPort)

    #imgStack = cvzone.stackImages([img, imgColor, mask, imgContour], 2, 0.5)
    imgContour = cv2.resize(imgContour, (0,0), None, 0.3, 0.3)
    cv2.imshow("ImageContour", imgContour)
    cv2.waitKey(1)