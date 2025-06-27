RM = del /Q
ST = cmd /C start ""

build:
	g++ -std=c++17 main.cpp Utils/*.cpp -o rec.exe

remove:
	$(RM) rec.exe

run:
	$(ST) .\rec.exe