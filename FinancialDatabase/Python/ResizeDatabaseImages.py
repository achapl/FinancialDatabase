# Will resize all images in the database that are not resized to thumbnails,
# And insert those thumbnails into the images table in the database,
# With the original image's thumbnailID pointing to the new smaller thumbnail version

from Connection.DtbConnAndQuery import runQuery
from PIL import Image
import os
import io

imageDir = "C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/"
maxDims = (300, 300)

f = None

# Get all image and image IDs, where there is no thumbnail ID

def getImagesAndData():
    imagesAndData, colNames, lastColID = runQuery("SELECT IMAGE_ID, image FROM image WHERE thumbnailID IS NULL")
    return imagesAndData

# Convert bytearray to image
def byteArrToImage(byteArr):
    image = Image.open(io.BytesIO(byteArr))
    
    return image

# Resize Image

def resizeImage(image):
    image.thumbnail(maxDims)
    return image

# Save Image

def getImagePath(name, image):
    return imageDir + str(name) + "." + image.format

def saveImage(image, imagePath):
    image.save(imagePath)

# Insert new image into database / Get result ID
def insertImage(imagePath):
    #a, b, lastRowID = runQuery("INSERT INTO thumbnail (thumbnail) VALUES (LOAD_FILE('" + imagePath + "'));")
    #return lastRowID
    f.write("INSERT INTO thumbnail (thumbnail) VALUES (LOAD_FILE('" + imagePath + "'));\n")
    


# Update old image ID to reflect new thumbnail ID

def updateID(imageID, thumbnailID):
    #runQuery("UPDATE image SET thumbnailID = last_insert_id() WHERE IMAGE_ID = " + str(imageID))
    f.write("UPDATE image SET thumbnailID = last_insert_id() WHERE IMAGE_ID = " + str(imageID) + ";\n")
   
runQuery("SELECT LOAD_FILE('C:\\ProgramData\\MySQL\\MySQL Server 8.0\\Uploads\\0.JPEG');")
#runQuery("INSERT INTO thumbnail (thumbnail) VALUES (LOAD_FILE('C:\\ProgramData\\MySQL\\MySQL Server 8.0\\Uploads\\0.JPEG'));")


try:
    f = open("C:/users/owner/Desktop/InsertThumbnails.sql",'x')
except:
    f = open("C:/users/owner/Desktop/InsertThumbnails.sql",'w')
imagesAndData = getImagesAndData()

count = 0
for row in imagesAndData:
    imageID = row[0]
    byteImage = row[1]
    convertedIm = bytes(byteImage)

    image = byteArrToImage(convertedIm)
    image = resizeImage(image)
    
    imagePath = getImagePath(count, image)
    saveImage(image, imagePath)

    #thumbnailID = insertImage(imagePath)
    thumbnailID = -1
    insertImage(imagePath)
    updateID(imageID, thumbnailID)

    count += 1
    