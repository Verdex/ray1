
function crc_table()
    local t = {}

    for i = 0, 255 do
        t[i] = 0
    end

    for n = 0, 255 do
        local c = n
        for k = 0, 7 do
            if  (c & 1) == 1 then
                c = 0xedb88320 ~ ( c >> 1 )
            else
                c = c >> 1
            end
            t[n] = 0xFFFFFFFF & c
        end
    end
    return t
end

function display( t )
    local d = {} 
    local c = 0
    for i = 0, #t  do
        c = c + 1
        d[#d + 1] = t[i] 
    end
    return "{ " .. table.concat( d, ", " ) .. " }"
end

print( display( crc_table() ) )
--[[rc_table(void)
{
    unsigned long c;
    int n, k;
                                  
    for (n = 0; n < 256; n++) {
        c = (unsigned long) n;
        for (k = 0; k < 8; k++) {
            if (c & 1)  c = 0xedb88320L ^ (c >> 1);
            else
                c = c >> 1;   
            }
           crc_table[n] = c;
        }
    crc_table_computed = 1;
}
--]]
