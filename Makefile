
cc = csc
files = Image.cs

all: view.exe ray.exe

ray.exe:
	$(cc) $(files) Program.cs -t:exe -out:Ray.exe 

view.exe:
	$(cc) view/Program.cs -t:exe -out:View.exe

clean:
	rm -rf *.exe 
