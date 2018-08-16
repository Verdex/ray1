
cc = csc
assemblies = System.Windows.Forms

all:
	$(cc) Program.cs -t:exe -o:Display -r:$(assemblies)
