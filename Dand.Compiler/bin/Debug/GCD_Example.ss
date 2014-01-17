begin
var $a as int
var $b as int
var $t as int
var $c as bool

print "Welcome To GCD !!!"

print "Input a:"
input $a
print "Input b:"
input $b

if ($a > 0) then
print $b
end

$c = false
if ($c == true) then
print 12345
end

while ($b!=0) do
$t=$b
$b=$a % $b
$a=$t
end
print "The GCD is:"
print $a
end