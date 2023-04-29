pre = '#if UNITY_EDITOR\n'
end = '#endif'

import os

def add(path):
    print(path)
    with open(path, 'rt', encoding='utf_8_sig') as f:
        lines = f.readlines()
        if (lines[0] != pre):
            lines = [pre] + lines + [end]
            if (not lines[-2].endswith('\n')):
                lines[-2] += '\n'
    with open(path, 'wt', encoding='utf_8_sig') as f:
        f.writelines(lines)

for filepath, dirnames, filenames in os.walk(r'D:\TorappuMap(1.9.91)\Assets\CustomMap\Scripts'):
    for filename in filenames:
        if (filename.endswith('.cs')):
            path = os.path.join(filepath, filename)
            add(path)