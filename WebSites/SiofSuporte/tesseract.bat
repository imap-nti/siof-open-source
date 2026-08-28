@echo off
for %%A in ("%~1\*.png") do C:\tesseract\tesseract.exe %%~fA stdout -l por quiet