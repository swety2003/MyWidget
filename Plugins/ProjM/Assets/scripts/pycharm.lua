

baseDir = "D:\\Source\\PythonProjects"
item.Icon = Path.Combine(selfp, "Assets/icon/pycharm-app.ico")
item.Name = "Pycharm"

local fileico = Path.Combine(selfp, "Assets/icon/python.ico")

function OnUpdate()
    local di = Directory.GetDirectories(baseDir);
    for i=0,di.Length-1 do
        local f= Directory.GetDirectories(di[i]);
        for j=0,f.Length-1 do
            local str = f[j].lower(f[j])
            if (string.sub(str,-5)==".idea") then
                path=di[i]
                item:AddInfo(Path.GetFileName(path),path,fileico)
            end
        end
    end
end

function OnActive(info)
    Process.Start("C:\\Applications\\JetBrains\\PyCharm Community Edition 2022.3.1\\bin\\pycharm64.exe", info.path);
end

