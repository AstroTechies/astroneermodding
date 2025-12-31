local UEHelpers = require("UEHelpers")
local AstroHelpers = require("AstroHelpers")

function go()
    print("Logging")
    local a, b, c = UE4SS:GetVersion()
    local all_mods = ""
    local test2 = {}
    AstroHelpers:GetIntegratorAPI():GetAllModNames(UEHelpers:GetWorldContextObject(), test2)
    for i = 1, #test2 do
        all_mods = all_mods .. test2[i]:get():ToString() .. " "
    end

    local f = io.open("UE4SS_AML_TEST.log", "w")
    f:write("Welcome to UE4SS!\n")
    f:write("UE4SS: " .. a .. "." .. b .. "." .. c .. "\n")
    f:write("Astroneer: " .. AstroHelpers.GetGameVersion() .. "\n")
    f:write("Integrator: " .. AstroHelpers.GetIntegratorVersion() .. "\n")
    f:write("Mods: " .. all_mods .. "\n")
    f:close()
end

NotifyOnNewObject("/Script/Engine.PlayerController", go)
