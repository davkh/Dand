function $func($n as int)
begin 
if($n < 2) then
return 1
end
return $func($n - 1) + $func($n - 2)
end

begin
var $n as int
$n = 10
end