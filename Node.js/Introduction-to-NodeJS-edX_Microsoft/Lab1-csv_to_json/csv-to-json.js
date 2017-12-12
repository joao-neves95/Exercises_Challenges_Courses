const fs = require('fs')
const path = require('path')
const csvPATH = path.normalize('./customer-data.csv')

const csvToJson = (csvPATH) => {

  const readCSV = (csvPATH, callback) => {
    console.log('Reading ' + csvPATH + '...')

    // Read the .csv file:
    fs.readFile(csvPATH, 'utf8', (error, data) => {
      if (error)
        callback(error, null)
      callback(null,data)
    })
  }

  // Manipulate the "data" callback from readCSV():
  readCSV(csvPATH, (error, data) => {
    console.log('Converting ' + csvPATH + ' data content to JSON format...')

    // Convert csv into JSON format:
    const parseCsvToJson = (data) => {
      const lines = data.split('\n')
      const keys = lines[0].split(',')

      let json = []

      for (let i = 1; i < lines.length - 1; i++) {
        let obj = {}
        let lineValues = lines[i].split(',')

        for (let j = 0; j < keys.length; j++) {
          // Make object and remove '\n' characters:
          obj[keys[j].replace(/\r?\n|\r/g,'')] = lineValues[j].replace(/\r?\n|\r/g,'')
        }
        // Push each JSON object into the "json" array:
        json.push(obj)
      }
      return JSON.stringify(json)
    }

    // Extract file name, and append new extension:
    const fileName = path.basename(csvPATH, '.csv') + '.json'

    // Write the .json file, with the parsed data from parseCsvToJson() function:
    fs.writeFile(path.join(__dirname, fileName), parseCsvToJson(data), (error) => {
      console.log('Creating the .json file...')
      if (error)
        throw error
      console.log('The ' + fileName + ' file has been created')

    })
  })
}

csvToJson(csvPATH)
