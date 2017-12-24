'use strict'
const mongodb = require('mongodb')
const fs = require('fs')
const path = require('path')
const async = require('async')

const url = 'mongodb://localhost:27017/bitcoin-bank'
const customerPath = require('./data/m3-customer-data.json')
const customerAddressesPath = require('./data/m3-customer-address-data.json')

let tasks = []
const limit = parseInt(process.argv[2], 10) || 1000
console.log('Limit: ', parseInt(process.argv[2], 10) || 1000)

// MongoDB connection:
mongodb.MongoClient.connect(url, (err, database) => {
  if (err)
    throw err

  const dbase = database.db('bitcoin-bank')

  customerPath.forEach((customer, index, list) => {
    // Add the customerAddresses to each customer object:
    customerPath[index] = Object.assign(customer, customerAddressesPath[index])

    if (index % limit === 0) {
      const start = index
      let end
      if (start > customerPath.length) {
        end = customerPath.length - 1
      }
      end = start + limit

      tasks.push((done) => {
        console.log(`Processing ${start} - ${end} out of ${customerPath.length}`)
        dbase.collection('customers')
             .insert(customerPath.slice(start, end), (err, results) => {
               done(err, results)
             })
      })
    }
  })

  console.log(`Launching ${tasks.length} parallel task/s...`)
  const startTime = Date.now()
  async.parallel(tasks, (err, results) => {
    if (err)
      throw err

    const endTime = Date.now()
    console.log(`Execution time: ${endTime - startTime}`)
    // console.log(results)
    database.close()
    process.exit(1)
  })
})
