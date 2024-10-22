from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.common.by import By
import requests
import os
import time
from PIL import Image
from io import BytesIO

# Ruta al archivo ChromeDriver
chrome_driver_path = "E:/Documentos/Estudios/Erasmus/ML_Project_Artstyle_Prediction/ML_ArtStyleTrainingData/Ml_Testing_Data/Downloaded_Data/chromedriver-win64/chromedriver.exe"

# Inicializa el navegador Chrome usando el ChromeDriver
service = Service(chrome_driver_path)
driver = webdriver.Chrome(service=service)

# Abre la página de WikiArt
url = "https://www.wikiart.org/en/paintings-by-style/surrealism?select=featured#!#filterName:featured,viewType:masonry"
driver.get(url)

# Espera unos segundos para que cargue el contenido
driver.implicitly_wait(5)

# Intentar hacer clic en "Load More" varias veces
for _ in range(200):  # Puedes ajustar el número de veces que intentas hacer clic
    try:
        load_more_button = driver.find_element(By.CLASS_NAME, "masonry-load-more-button")
        load_more_button.click()  # Clic en el botón "Load More"
        time.sleep(3)  # Espera a que carguen más imágenes
    except:
        print("No se encontró el botón 'Load More' o ya no hay más imágenes que cargar.")
        break


# Crear una carpeta para almacenar las imágenes descargadas
if not os.path.exists('images_surrealism'):
    os.makedirs('images_surrealism')

# Encuentra todas las imágenes en la página
imagenes = driver.find_elements(By.TAG_NAME, "img")

# Descargar cada imagen, solo si tiene un tamaño mayor que 1x1
for idx, img in enumerate(imagenes):
    src = img.get_attribute("src")
    if src:
        try:
            img_data = requests.get(src).content
            img_file = Image.open(BytesIO(img_data))
            
            # Comprobar si el tamaño de la imagen es mayor que 1x1
            if img_file.size[0] > 1 and img_file.size[1] > 1:
                with open(f"images_surrealism/imagen_{idx}.jpg", 'wb') as handler:
                    handler.write(img_data)
                print(f"Imagen {idx} descargada.")
            else:
                print(f"Imagen {idx} ignorada por ser de tamaño {img_file.size}.")
        
        except Exception as e:
            print(f"Error al procesar la imagen {idx}: {e}")

# Cerrar el navegador
driver.quit()
