# C:\Python27\python.exe server.py
import socket

import base64
import requests


serv = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
serv.bind(('0.0.0.0', 8080))
serv.listen(5)

print('!listening for connections...')

buffer_size = 4096
image_path = 'received_image.jpg'
secret_key = 'sk_e78b13b44b250ebc3d9f61ff'
url = ('https://api.openalpr.com/v2/recognize_bytes?recognize_vehicle=1&country=eu&secret_key={}'.format(secret_key))

while True:
	conn, addr = serv.accept()
	print('Connected to: ' + str(addr))
	#i = 0;
	# receive the image from android
	with open(image_path, 'wb') as f:
		data = conn.recv(buffer_size)
		while data:
			#print('writing ' + str(i) + ': ' + str(len(data)))
			if data.endswith("endoffile"):
				data = data.replace("endoffile", "")
				f.write(data)
				break
			else:
				f.write(data)
			#i = i + 1
			data = conn.recv(4096)
			# print(data)
	print('received data')
	
	# get the response from openalpr
	with open(image_path, 'rb') as image_file:
		img_base64 = base64.b64encode(image_file.read())
	r = requests.post(url, data=img_base64)
	
	# print out the received response
	# print('Raw json received from openalpr:\n' + str(r.json()))
	try:
		plate = r.json()['results'][0]['plate']
	except:
		plate = 'unknown'
	print('Plate is: ' + plate);
	
	# Send the repsonce to android
	conn.send(plate)
	conn.close()
	print 'client disconnected'
	break;
	
with open("plate.txt", 'w') as f:
	f.write(plate)