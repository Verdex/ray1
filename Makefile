
cc = csc
convert = convert/Image.cs convert/Png.cs convert/Program.cs
util = util/Crc.cs

all: View.exe Ray.exe Convert.exe

Ray.exe:
	$(cc)  Program.cs -t:exe -out:Ray.exe 

View.exe: Util.netmodule
	$(cc) view/Program.cs -addmodule:Util.netmodule -t:exe -out:View.exe

Convert.exe: Util.netmodule
	$(cc) $(convert) -t:exe -out:Convert.exe

Util.netmodule:
	$(cc) $(util) -t:module -out:Util.netmodule

clean:
	rm -rf *.exe 
	rm -rf *.netmodule
