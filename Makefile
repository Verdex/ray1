
cc = csc
convert = convert/Image.cs convert/Png.cs convert/Program.cs convert/Parser.cs convert/Ppm.cs
util = util/Crc.cs util/Zip.cs
standard = standard/Util.cs standard/Geom.cs standard/ICollidable.cs standard/Program.cs

all: View.exe Ray.exe Convert.exe Standard.exe

Ray.exe:
	$(cc)  Program.cs -t:exe -out:Ray.exe 

View.exe: Util.netmodule
	$(cc) view/Program.cs -addmodule:Util.netmodule -t:exe -out:View.exe

Convert.exe: Util.netmodule
	$(cc) $(convert) -addmodule:Util.netmodule -t:exe -out:Convert.exe

Standard.exe: 
	$(cc) $(standard) -t:exe -out:Standard.exe

Util.netmodule:
	$(cc) $(util) -t:module -out:Util.netmodule

clean:
	rm -rf *.exe 
	rm -rf *.netmodule
