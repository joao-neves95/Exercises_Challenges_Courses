// Identity Matrices:
let I1 = [[1]]
let I2 = [[1, 0], [0, 1]]
let I3 = [[1, 0, 0], [0, 1, 0], [0, 0, 1]]
let I4 = [[1, 0, 0, 0], [0, 1, 0, 0], [0, 0, 1, 0], [0, 0, 0, 1]]

// Matrix Size:
function  matrixSize (matrix) {
  let size = []
  // Rows:
  size.push(matrix.length)
  // Columns:
  size.push(matrix[0].length)
  return size
}

// Determinant of a Matrix:
function det (matrix) {
  if (matrix.length === 1) {
    return matrix[0][0]
  } else if (matrix.length === 2) {
    return (matrix[0][0] * matrix[1][1]) - (matrix[0][1] * matrix[1][0])
  } else {
    console.log('Matrices.js does not yet support the calculation of determinants of matrices bigger than 2*2')
    return null
  }
}

// Create a Random Matrix:
function randomMatrix (i, j, min, max) {
  let matrix = []
  let row = []
  for (let rowsCount = 0; rowsCount <= i - 1; rowsCount++) {
    for (let colsCount = 0; colsCount <= j - 1; colsCount++) {
      row.push(Math.floor((Math.random() * (max + 1) + min)))
      if (colsCount === j - 1) {
        matrix.push(row)
        row = []
      }
    }
  }
  return matrix
}


/*
Examples:
_________

let m = [
[4, 6], 
[7, 9]
]

let m2 = [
[ 1,  3, 0], 
[-4, -2, 1],
[ 3, -1, 5]
]

let m3 = [
[1],
[2],
[3]
]

---------------------------------------------
// Identity Matrices:
I3 // [ [ 1, 0, 0 ], [ 0, 1, 0 ], [ 0, 0, 1 ] ]
(...)

I[1-4]
---------------------------------------------
matrixSize(m)         // [2, 2]

matrixSize(m2)        // [3, 3]

matrixSize(m3)        // [3, 1]
---------------------------------------------
det(m)                // -6

det([
[4, 6], 
[7, 9]
])                    // -6

// Matrices.js does not yet support the calculation of determinants of matrices bigger than 2*2
// Matrices.js does not yet support the calculation of determinants of non-square matrices (n * n)
---------------------------------------------
randomMatrix(rows, cols, (range of the random numbers:) min, max)

randomMatrix(3, 5, 0, 10)             // [ [ 1, 5, 6, 3, 7 ], [ 8, 1, 5, 1, 2 ], [ 4, 0, 3, 3, 7 ] ]

matrixSize(randomMatrix(3, 5, 0, 1))  // [3, 5]
*/
