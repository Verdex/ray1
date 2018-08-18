
cc = csc
ui = MainForm.cs
assemblies = System.Windows.Forms.dll 

all: view.exe

#	$(cc) $(ui) Program.cs -t:exe -out:Display.exe -r:$(assemblies)

view.exe:
	$(cc) view/Program.cs -t:exe -out:View.exe

clean:
	rm -rf *.exe 
