import os
from PIL import Image
import tkinter
import tkinter.font as tf
from tkinter import filedialog

root=tkinter.Tk()

def picFile():
    Filepath = filedialog.askopenfilename(filetypes =[('图片文件',"*.jpg;*.png;*.bmp;*.jpeg"), ('All Files', '*')]) #获得选择好的文件
    bef_text.delete('1.0','end')
    bef_text.insert(tkinter.INSERT,Filepath)
    

def picFiles():
    Filepath = filedialog.askdirectory()    #获得选择好的文件夹
    bef_text2.delete('1.0','end')
    bef_text2.insert(tkinter.INSERT,Filepath)

def picFiles2():
    Filepath = filedialog.askdirectory()    #获得选择好的文件夹
    bef_text3.delete('1.0','end')
    bef_text3.insert(tkinter.INSERT,Filepath)
    
def openfile():
    str5 = bef_text3.get('0.0','end')
    str6 = str5[:len(str5)-1]
    os.startfile(str6)

def file_name(file_dir):   
    for root, dirs, files in os.walk(file_dir):  
        #print(root) #当前目录路径  
        #print(dirs) #当前路径下所有子目录  
        #print(files) #当前路径下所有非目录子文件
        #files.sort(key=lambda x:int(x[:-4]))
        #print(files) #当前路径下所有非目录子文件
        return files

def picChange():
    str = bef_text.get('0.0','end')
    if len(str) == 1:
        picFile()
    file = os.path.split(str)
    if len(bef_text3.get('0.0','end')) != 1:
        str3 = bef_text3.get('0.0','end')
        str4 = str3[:len(str3)-1]
        out = str4 + '/' + file[1][0:-5] + '_new.png'
    else:
        out = file[0] + '/' + file[1][0:-5] + '_new.png'

    doPicChange(str,out)
    tkinter.messagebox.showinfo('转换成功')

def doPicChange(filePath,outPath):
    img = Image.open(filePath)
    img = img.convert("RGBA")
    bands = img.split()
    rIm = bands[0]
    gIm = bands[1]
    bIm = bands[2]
    aIm = bands[3]
    width, height = img.size
    for x in range(width):
        for y in range(height):
            aIm.putpixel((x,y), int((bIm.getpixel((x, y)) + gIm.getpixel((x, y)) + bIm.getpixel((x, y))) / 3))
    nimg = Image.merge("RGBA", (rIm, gIm, bIm, aIm))
    nimg.save(outPath)

def picChangeBat():
    str2 = bef_text2.get('0.0','end')
    if len(str2) == 1:
        picFiles()
    str2 = str2[:len(str2)-1]

    listPic = file_name(str2)

    if len(bef_text3.get('0.0','end')) != 1:
        str3 = bef_text3.get('0.0','end')
        str4 = str3[:len(str3)-1]

        for i ,v in enumerate(listPic):
            out = str4 + '/' + listPic[i][0:-4] + '_new.png'
            filename = str2 + '/' + listPic[i]
            doPicChange(filename,out)
    else:
        for i ,v in enumerate(listPic):
            out = str2 + '/' + listPic[i][0:-4] + '_new.png'
            filename = str2 + '/' + listPic[i]
            doPicChange(filename,out)
    tkinter.messagebox.showinfo('转换成功')



root.title('黑底素材转透明')     #title
root.geometry('620x240')       #这里的乘号是英文字母x,不是*
#选择前-文本框
bef_text = tkinter.Text(width = 28,height = 2,font = tf.Font(family='萝莉体',size=15))
bef_text.place(relx = 0.05,rely = 0.08)
bef_text.bind('<KeyPress>',lambda e:'break')
#选择前-图片位置
atButton = tkinter.Button(root,text = '图片位置',command = picFile,font = tf.Font(family='萝莉体',size=15))
atButton.place(relx = 0.62,rely = 0.1)
#选择后-文字识别
atButton = tkinter.Button(root,text = '开始转换',command = picChange,font = tf.Font(family='萝莉体',size=15))
atButton.place(relx = 0.82,rely = 0.1)

#选择前-文件夹文本框
bef_text2 = tkinter.Text(width = 28,height = 2,font = tf.Font(family='萝莉体',size=15))
bef_text2.place(relx = 0.05,rely = 0.38)
bef_text2.bind('<KeyPress>',lambda e:'break')
#选择前-文件夹位置
atButton = tkinter.Button(root,text = '批量位置',command = picFiles,font = tf.Font(family='萝莉体',size=15))
atButton.place(relx = 0.62,rely = 0.4)
#选择后-图片批量识别
atButton = tkinter.Button(root,text = '批量转换',command = picChangeBat,font = tf.Font(family='萝莉体',size=15))
atButton.place(relx = 0.82,rely = 0.4)

#输出路径
bef_text3 = tkinter.Text(width = 28,height = 2,font = tf.Font(family='萝莉体',size=15))
bef_text3.place(relx = 0.05,rely = 0.68)
bef_text3.bind('<KeyPress>',lambda e:'break')
#选择输出文件
atButton = tkinter.Button(root,text = '输出位置',command = picFiles2,font = tf.Font(family='萝莉体',size=15))
atButton.place(relx = 0.62,rely = 0.7)
#打开输出文件
atButton = tkinter.Button(root,text = '打开文件',command = openfile,font = tf.Font(family='萝莉体',size=15))
atButton.place(relx = 0.82,rely = 0.7)

#窗口的内容
root.mainloop() 