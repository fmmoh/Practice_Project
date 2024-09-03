function areaFromCoords(coordArray) {
  var x = coordArray,
    a = 0;

  // Must have even number of elements
  if (x.length % 2) return;

  // Process pairs, increment by 2 and stop at length - 2
  for (var i = 0, iLen = x.length - 2; i < iLen; i += 2) {
    a += x[i] * x[i + 3] - x[i + 2] * x[i + 1];
  }
  return Math.abs(a / 2);
}

let result = "Area is: " + areaFromCoords([1, 1, 3, 1, 3, 3, 1, 3, 1, 1]); // 4

document.getElementById("result").innerHTML = result;
