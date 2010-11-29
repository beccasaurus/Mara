function iReturnStaticString() {
  return 'static string!';
}

function iReturnStringUsingParameterPassed(param) {
  return 'you passed: ' + param;
}

function iReturnStringUsingParametersPassed() {
  var args = [];
  for (var i in arguments) args[args.length] = arguments[i];
  return 'you passed: ' + args.join(', ');
}

$(document).ready(function(){
    
});
