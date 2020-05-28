// Matrix Reviews: https://www.mathsisfun.com/algebra/matrix-multiplying.html

#include <iostream>
#include <cstdlib>

using namespace std;

#define MAXDATA 1024

typedef struct
{
    double *data;
    int nrows;
    int ncols;

} Matrix;

Matrix *createMatrix(int nrows, int ncols);
void destroyMatrix(Matrix *M);
void printmat(Matrix *M);
void matrixmult(Matrix *A, Matrix *B, Matrix *C);

void log(string content);
void logLine(string content);
void logEndl();
void logOpenBracket();
void logCloseBracket();

int main(int argc, char *argv[])
{
    Matrix *A = createMatrix(3, 2);
    A->data[0] = 1.2;
    A->data[1] = 2.3;
    A->data[2] = 3.4;
    A->data[3] = 4.5;
    A->data[4] = 5.6;
    A->data[5] = 6.7;
    printmat(A);

    Matrix *B = createMatrix(2, 3);
    B->data[0] = 5.5;
    B->data[1] = 6.6;
    B->data[2] = 7.7;
    B->data[3] = 1.2;
    B->data[4] = 2.1;
    B->data[5] = 3.3;
    printmat(B);

    Matrix *C = createMatrix(3, 3);
    matrixmult(A, B, C);
    printmat(C);

    destroyMatrix(A);
    destroyMatrix(B);
    destroyMatrix(C);

    system("pause");
    return 0;
}

// your code goes below...

Matrix *createMatrix(int nrows, int ncols)
{
    // fill in the code here
    Matrix *matrix = new Matrix();
    matrix->ncols = ncols;
    matrix->nrows = nrows;
    matrix->data = (double*)calloc(nrows + ncols, sizeof(int));
    return matrix;
}

void destroyMatrix(Matrix *M)
{
    // fill in the code here
    free( M->data );
    delete M;
    M = nullptr;
}

void printmat(Matrix *M)
{
    // fill in the code here
    logOpenBracket();
    logEndl();

    int iRow;
    int iCol;
    for (iRow = 0; iRow < M->nrows; ++iRow) {
        log(" ");
        logOpenBracket();

        for (iCol = 0; iCol < M->ncols; ++iCol) {
            cout << M->data[(iRow * M->ncols) + iCol];

            if (iCol < M->ncols - 1) {
                log(", ");
            }
        }

       logCloseBracket();

       if (iRow < M->nrows - 1) {
           log(", ");
       }

       logEndl();
    }

    logCloseBracket();
    logEndl();
}

/*

let i & j start at 1;
let n = A cols, m = B rows => n = m
for i = 1, ..., n & j = 1, ..., m;

C = (Ai1 . B1j) + (Ai2 . B2j) + ... + (Ain . Emj) = E (Ain . Bmj)

*/
void matrixmult(Matrix *A, Matrix *B, Matrix *C)
{
    // fill in the code here
    if (A->ncols != B->nrows) {
        log("The number of rows of A must be equal to the collumns in B.");
        return;
    }

    int i = 0;
    int n = 0;
    int j = 0;
    int cIndex = 0;
    for (i = 0; i < A->nrows; ++i) {
        for (j = 0; j < B->ncols; ++j) {
            for (n = 0; n < B->nrows; ++n) {
                C->data[cIndex] += (A->data[(i * A->ncols) + n] * B->data[(n* B->ncols) + j]);
            }

            ++cIndex;
        }
    }
}

// #region HELPER METHODS

void log(string content) {
    cout << content;
}

void logEndl() {
    log("\n");
}

void logLine(string content) {
    log(content);
    logEndl();
}

void logOpenBracket() {
    log("[");
}

void logCloseBracket() {
    log("]");
}

// #endregion HELPER METHODS
