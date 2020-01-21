
#include <stdio.h>
#include <locale.h>

#define INPUT_SIZE 10

void askGrades(int *inputNumArr, size_t quantity);
void printGradesMeta(int *inputNumArr, size_t arrSize);
int askMinimumGrade();
void printGradesHigherThan(int *numsArr, int minimumNum, size_t arrSize);

int main()
{
    setlocale(LC_ALL, "");

	int nums[INPUT_SIZE];

	askGrades(nums, INPUT_SIZE);
	printGradesMeta(nums, INPUT_SIZE);
	int minimumGrade = askMinimumGrade();
	printGradesHigherThan(nums, minimumGrade, INPUT_SIZE);
    printf("\n\n");
    
    system("pause");
    return 0;
}

/*
	Asks for n (arrSize) numbers and returns the sum.
*/
void askGrades(int *inputNumArr, size_t arrSize) {
	int i = 0;

	do {
		printf("Escreva um número entre 0 e 20: ");
		scanf("%d", inputNumArr + i);

		if ( *(inputNumArr + i) < 0 || *(inputNumArr + i) > 20) {
			printf("O número tem que ser entre 0 a 20! \n");

		} else {
			++i;
		}

	} while (i < arrSize);
}

void printGradesMeta(int *inputNumArr, size_t arrSize) {
	int currentNum;
	int sum = 0;
	int highest = 0;
	int lowest = 20;

	int i;
	for (i = 0; i < arrSize; ++i) {
		currentNum = *(inputNumArr + i);
		sum += currentNum;
		
		if (currentNum < lowest) {
			lowest = currentNum;
		}
		
		if (currentNum > highest) {
			highest = currentNum;
		}
	}
	
	printf("\n\nA melhor nota é: %d. \n", highest);
	printf("A pior nota é: %d. \n", lowest);
	printf("A média das notas é: %.2f. \n\n", (float)sum / arrSize);
}

int askMinimumGrade() {
	int minimumGrade;
	
	printf("\n\nEscreva a nota mínima que deseja que seja mostrada: ");

	do {
	    scanf("%d", &minimumGrade);
	    
	    if (minimumGrade < 0 ||  minimumGrade > 20) {
	    	printf("O número tem que ser entre 0 a 20! \n");
		}
	    
    } while( minimumGrade < 0 ||  minimumGrade > 20 );
    
    return minimumGrade;
}

void printGradesHigherThan(int *numsArr, int minimumNum, size_t arrSize) {
	int i = 0;
	while(i < arrSize) {
		if ( *(numsArr + i) >= minimumNum ) {
			printf( "%d ", *(numsArr + i) );
		}
		
		++i;
    }
}

