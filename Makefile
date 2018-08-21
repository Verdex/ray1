
cc = csc
files = Image.cs Png.cs
assemblies = System.Data.dll

all: View.exe Ray.exe

Ray.exe:
	$(cc) $(files) Program.cs -t:exe -out:Ray.exe -r:$(assemblies)

View.exe: Util.netmodule
	$(cc) view/Program.cs -addmodule:Util.netmodule -t:exe -out:View.exe

Util.netmodule:
	$(cc) util/Crc.cs -t:module -out:Util.netmodule

clean:
	rm -rf *.exe 
	rm -rf *.netmodule
