from tkinter import *
import random as ran
import math
root = Tk()
isinx = []
isino = []
x = PhotoImage(file = "x.gif")
o = PhotoImage(file = "o.gif")
def Tell(g):
	global isino
	global isinx 
	Yes = 0
	for i in g:
		if i == buttons[1]:
			if buttons[2] in g and buttons[3] in g:
				Yes = 1
		elif i == buttons[4]:
			if buttons[5] in g and buttons[6] in g:
				Yes = 1
		elif i == buttons[7]:
			if buttons[8] in g and buttons[9] in g:
				Yes = 1
		elif i == buttons[1]:
			if buttons[4] in g and buttons[7] in g:
				Yes = 1
		elif i == buttons[2]:
			if buttons[5] in g and buttons[8] in g:
				Yes = 1
		elif i == buttons[3]:
			if buttons[6] in g and buttons[9] in g:
				Yes = 1
		elif i == buttons[1]:
			if buttons[5] in g and buttons[9] in g:
				Yes = 1
		elif i == buttons[3]:
			if buttons[5] in g and buttons[7] in g:
				Yes = 1
		if Yes == 1:
			if x == isinx:
				return "You win"
		if Yes == 1:
			if x == isino:
				return "CPU wins"
def click(event):
    global buttons
    global content
    global k
    global isinx
    global isino
    global Help
    global head
    current = event.widget
    if current not in Help:
        current.config(image = x, height = 148, width = 148)
        Help.append(current)
        isinx.append(current)
        if Tell(isinx) == "You win" :
        	content.set("You Win!")
        else:
        	if len(Help) != 9:
                        k = ran.randrange(1, 10)
                        while buttons[k] in Help:
                                k = ran.randrange(1, 10)
                        buttons[k].config(image = o, height = 148, width = 148)
			Help.append(buttons[k])
			isino.append(buttons[k])
			if Tell(isino) == "CPU wins":
				content.set("CPU Won, Sorry")

buttons = {}
Help = []

g = Label(root, image = x)

content = StringVar()
head = Entry(root, text=content, font=("Arial", 36))
head.grid(row=0, column=0, columnspan=3)
content.set("Your turn")
for i in range(1, 10):
    buttons[i] = Button(root, width = 16, height = 8, font = ("Times", 12))
    buttons[i].grid(row = math.ceil(i/3), column = (i-1)%3, sticky = "wesn")
    buttons[i].bind("<Button-1>", click)
root.mainloop()
