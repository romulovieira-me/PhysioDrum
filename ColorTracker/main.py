import cvzone
from cvzone.ColorModule import ColorFinder
import cv2
import socket  # Permite o envio de informações para o Unity

# Iniciando a webcam
#cap = cv2.VideoCapture(0)
cap = cv2.VideoCapture(1, cv2.CAP_DSHOW)  # Para Windows
cap.set(3, 1280)
cap.set(4, 720)

success, img = cap.read()
h, w, _ = img.shape

# Descobrindo a cor dos objetos
myColorFinder_rosa = ColorFinder(False)  # Para rastrear o objeto rosa
hsvVals_rosa = {'hmin': 155, 'smin': 24, 'vmin': 165, 'hmax': 169, 'smax': 203, 'vmax': 255}  # Rosa

myColorFinder_verde = ColorFinder(False)  # Para rastrear o objeto verde
hsvVals_verde = {'hmin': 25, 'smin': 45, 'vmin': 47, 'hmax': 91, 'smax': 255, 'vmax': 188}  # Verde

# Sockets para enviar informações
sock_rosa = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort_rosa = ("127.0.0.1", 5053)  # Porta para o objeto rosa

sock_verde = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort_verde = ("127.0.0.1", 5054)  # Porta para o objeto verde

while True:
    success, img = cap.read()

    # Invertendo a imagem da câmera horizontalmente para corrigir o espelhamento
    img = cv2.flip(img, 1)

    # Rastreando o objeto rosa
    imgColor_rosa, mask_rosa = myColorFinder_rosa.update(img, hsvVals_rosa)
    imgContour_rosa, contours_rosa = cvzone.findContours(img, mask_rosa)

    if contours_rosa:
        data_rosa = contours_rosa[0]['center'][0], \
                    h - contours_rosa[0]['center'][1], \
                    int(contours_rosa[0]['area'])
        print("Rosa:", data_rosa)
        sock_rosa.sendto(str.encode(str(data_rosa)), serverAddressPort_rosa)

    # Rastreando o objeto verde
    imgColor_verde, mask_verde = myColorFinder_verde.update(img, hsvVals_verde)
    imgContour_verde, contours_verde = cvzone.findContours(img, mask_verde)

    if contours_verde:
        data_verde = contours_verde[0]['center'][0], \
                       h - contours_verde[0]['center'][1], \
                       int(contours_verde[0]['area'])
        print("Verde:", data_verde)
        sock_verde.sendto(str.encode(str(data_verde)), serverAddressPort_verde)

    # Exibição das imagens dos contornos para os dois objetos
    imgContour_rosa_resized = cv2.resize(imgContour_rosa, (0, 0), None, 0.3, 0.3)
    imgContour_verde_resized = cv2.resize(imgContour_verde, (0, 0), None, 0.3, 0.3)

    cv2.imshow("Rosa", imgContour_rosa_resized)
    cv2.imshow("Verde", imgContour_verde_resized)

    cv2.waitKey(1)
