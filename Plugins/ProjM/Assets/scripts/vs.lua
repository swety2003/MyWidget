

baseDir = "D:\\Source\\Repos"
item.Icon = Path.Combine(selfp, "Assets/icon/vs-app.ico")
item.Name = "VisualStudio"

local fileico = Path.Combine(selfp, "Assets/icon/vs-sln.ico")

function OnUpdate()
    local di = Directory.GetDirectories(baseDir);
    for i=0,di.Length-1 do
        local f= Directory.GetFiles(di[i]);
        for j=0,f.Length-1 do
            local str = f[j].lower(f[j])
            if (string.sub(str,-4)==".sln") then
                path=f[j]
                item:AddInfo(Path.GetFileName(path),path,fileico)
            end
        end
    end
end

function OnActive(info)
    Process.Start("explorer.exe", info.path);
end

