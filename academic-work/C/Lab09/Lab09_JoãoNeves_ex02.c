/*

By: João Neves (SHIVAYL)

*/
#include <stdio.h>
#include <locale.h>

#define CITY_NUM 5

typedef struct {
    char cityName[50];
    int celcius;
    int fahrenheit;

} CityTemperature;

typedef struct {
    // Because this is a simple program it's not necessary to make a function
    //   constructor of TemperatureGroup and malloc cityTemperatures.
    CityTemperature cityTemperatures[CITY_NUM];

    int sumCelciusTemp;
    int smallestTempCityIdx;
    int biggestTempCityIdx;

} TemperatureGroup;

void getInput(TemperatureGroup *cityGroup, const size_t cityNum);
void printCityMeta(TemperatureGroup *cityGroup, const size_t cityNum);

int main( int argc, const char* argv[] )
{
    setlocale(LC_ALL, "");
    
    TemperatureGroup cityGroup;
    cityGroup.sumCelciusTemp = 0;
    cityGroup.smallestTempCityIdx = 0;
    cityGroup.biggestTempCityIdx = 0;

    getInput(&cityGroup, CITY_NUM);
    printCityMeta(&cityGroup, CITY_NUM);

    printf("\n\n");
    system("pause");
	return 0;
}

void getInput(TemperatureGroup *cityGroup, const size_t cityNum) { 
    printf("Escreva o nome de %d cidades e as suas respetiva temperatura em graus Celcius. \n", cityNum);
    
    int i;
    for (i = 0; i < cityNum; ++i) {
        printf("\nNome da cidade nº %d: ", i + 1);
        scanf("%s", &cityGroup->cityTemperatures[i].cityName);
        
        printf("Temperatura em celcius da cidade \"%s\": ", cityGroup->cityTemperatures[i].cityName);
        scanf("%d", &cityGroup->cityTemperatures[i].celcius);
        
        if (cityGroup->cityTemperatures[i].celcius < cityGroup->cityTemperatures[cityGroup->smallestTempCityIdx].celcius) {
            cityGroup->smallestTempCityIdx = i;
        }
        
        if (cityGroup->cityTemperatures[i].celcius > cityGroup->cityTemperatures[cityGroup->biggestTempCityIdx].celcius) {
            cityGroup->biggestTempCityIdx = i;
        }
        
        // ºF = ºC * 1.8 + 32
        cityGroup->cityTemperatures[i].fahrenheit = cityGroup->cityTemperatures[i].celcius * 1.8 + 32;
        cityGroup->sumCelciusTemp += cityGroup->cityTemperatures[i].celcius;
    }
}

void printCityMeta(TemperatureGroup *cityGroup, const size_t cityNum) {
    printf("\n\n");
    
    printf("A cidade com a temperatura mais baixa é \"%s\", com %dºC. \n",
        cityGroup->cityTemperatures[cityGroup->smallestTempCityIdx].cityName,
        cityGroup->cityTemperatures[cityGroup->smallestTempCityIdx].celcius
    );
    
    printf("A cidade com a temperatura mais alta é \"%s\", com %dºC. \n",
        cityGroup->cityTemperatures[cityGroup->biggestTempCityIdx].cityName,
        cityGroup->cityTemperatures[cityGroup->biggestTempCityIdx].celcius
    );
    
    printf("A temperatura temperatura média é de %.2fºC.", (float)cityGroup->sumCelciusTemp / cityNum);
    printf("\n\n\n");
    
    printf("Estas são todas as cidades inseridas, juntamente com as respectivas temperaturas: \n\n");
    
    int i;
    for (i = 0; i < cityNum; ++i) {
        printf("Cidade: %s, ", cityGroup->cityTemperatures[i].cityName);
        printf("%dºC, ", cityGroup->cityTemperatures[i].celcius);
        printf("%dºF \n", cityGroup->cityTemperatures[i].fahrenheit);
    }
}

