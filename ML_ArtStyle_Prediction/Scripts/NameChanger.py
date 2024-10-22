import os

def renombrar_imagenes(carpeta):
    # Lista todos los archivos en la carpeta
    archivos = os.listdir(carpeta)
    
    # Filtrar solo las imágenes (puedes agregar o modificar las extensiones según necesites)
    imagenes = [archivo for archivo in archivos if archivo.endswith(('.png', '.jpg', '.jpeg', '.gif', '.bmp'))]
    
    # Renombrar cada imagen
    for idx, archivo in enumerate(imagenes, start=1):
        # Crear nuevo nombre
        nuevo_nombre = f"img_{idx}{os.path.splitext(archivo)[1]}"  # Mantiene la extensión original
        # Construir las rutas completas
        ruta_antigua = os.path.join(carpeta, archivo)
        ruta_nueva = os.path.join(carpeta, nuevo_nombre)
        
        # Renombrar el archivo
        os.rename(ruta_antigua, ruta_nueva)
        print(f"Renombrado: {archivo} a {nuevo_nombre}")

# Uso
carpeta = "E:\Documentos\Estudios\Erasmus\ML_Project_Artstyle_Prediction\ML_ArtStyle_Prediction\Scripts\images_surrealism"  # Reemplaza con la ruta de tu carpeta
renombrar_imagenes(carpeta)