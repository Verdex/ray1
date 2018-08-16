
cc = csc
assemblies = System.Windows.Forms.dll

all:
	$(cc) Program.cs -t:exe -out:Display.exe -r:$(assemblies)

clean:
	rm -rf *.exe 
