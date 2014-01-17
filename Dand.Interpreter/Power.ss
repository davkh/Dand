function $pow($x as int, $n as int)
begin
if ($n == 1) then
return 1
end
if ($n % 2 == 1) then
return $pow($x, $n / 2) * $pow($x, $n / 2) * $x
else
return $pow($x, $n / 2) * $pow($x, $n / 2)
end
end
begin
print $pow(2, 10)
end