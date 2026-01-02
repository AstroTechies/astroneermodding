import os, glob

extensions = ["*.rst", "*.json", "*.cs", "*.lua"]

output = "This file contains the full Astroneer Modding documentation, in concatenated .rst form.\n\n"
for filename in glob.glob("docs/*.rst"):
   print(filename)
   with open(filename, 'r', encoding="utf8") as f:
      output += filename + "\n" + f.read() + "\n\n"
for ext in extensions:
    for filename in glob.glob("docs/*/" + ext):
        print(filename)
        with open(filename, 'r', encoding="utf8") as f:
            output += filename + "\n" + f.read() + "\n\n"
      
with open("all_docs_as_txt.txt", 'w', encoding="utf8") as f:
    f.write(output)
