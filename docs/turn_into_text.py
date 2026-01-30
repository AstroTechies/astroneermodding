import glob, os

def turn_into_text_go():
    prefix = os.path.dirname(os.path.abspath(__file__))
    prefix_text = "================================================================================"
    extensions = ["*.rst", "*.json", "*.cs", "*.lua"]

    output = "This file contains the full Astroneer Modding documentation, in concatenated .rst form.\n\n"
    for filename in glob.glob(os.path.join(prefix, "*.rst")):
        #print(filename)
        with open(filename, 'r', encoding="utf8") as f:
            output += prefix_text + "\nFILE: " + os.path.basename(filename) + "\n" + prefix_text + "\n" + f.read() + "\n\n"
    for ext in extensions:
        for filename in glob.glob(os.path.join(prefix, "*/" + ext)):
            #print(filename)
            with open(filename, 'r', encoding="utf8") as f:
                output += prefix_text + "\nFILE: " + os.path.basename(filename) + "\n" + prefix_text + "\n" + f.read() + "\n\n"
        
    outPath = os.path.join(prefix, "all_docs_as_text.txt")
    with open(outPath, 'w', encoding="utf8") as f:
        f.write(output)
    print("Wrote to " + outPath)

if __name__ == '__main__':
    turn_into_text_go()
