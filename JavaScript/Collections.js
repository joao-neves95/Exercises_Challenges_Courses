/*

Class Dictionary(): let dictionary = new Dictionary(uniqueKeys = false)

Type safe Class List(): let list = new List('string' | 'number' | 'int' | 'float' | 'boolean')

*/

class Errors {
  static get existingKey() { throw new Error('An item with the same key has already been added.'); };

  static get noTypeProvided() { throw new Error('No type provided on Collection instantiation.') };

  static wrongType(type) { throw new Error(`The value is not from the same type as the List<${type}>`); };
}

class Collection {
  constructor(uniqueKeys, type) {
    this.elements = [];
    this.uniqueKeys = (uniqueKeys || false);

    if (!type) throw Errors.noTypeProvided;
    this.type = type;
  }


  get length() {
    return this.elements.length;
  };

  /**
   * Get all elements from the Collection.
   * Returns elements[]
   */
  getAll() {
    return this.elements;
  }

  /**
   * Remove all elements from the Collection.
   */
  clear() {
    this.elements = [];
  };

  /**
   * (private)
   * No type safety. For private class use.
   * @param {Type} value
   */
  push(value) {
    this.elements.push(value);
  }

  /**
    * (private)
    * No checks. For private class use.
    * @param {Number} index
    */
  splice(index) {
    this.elements.splice(index, 1);
  }
}

class Dictionary extends Collection {
  /**
   * Whether the keys should be unique or not.
   * Optional. It defaults to false.
   * @param {Boolean} uniqueKeys
   * @default {false}
   */
  constructor(uniqueKeys) {
    super(uniqueKeys, 'any');
  };

  getAllValues() {
    let allValues = [];

    for (let i = 0; i < this.elements.length; ++i) {
      allValues.push(Object.values(this.elements[i])[0]);
    }

    return allValues;
  }

  add(key, value) {
    if (this.uniqueKeys && this.findIndexOfKey(key) !== false)
      throw new Error(Errors.existingKey);

    this.push({ [key]: value });
  };

  remove(key) {
    const index = this.findIndexOfKey(key);
    if (!index)
      return false;

    this.splice(index);
  };

  /**
   * Get a value with its index. Returns an array with the values.
   * @param {number} index
   * @return {any[]}
   */
  getByIndex(index) {
    return Object.values( this.elements[index] )[0];
  };

  /**
   * Get a key with its index.
   * @param {number} index
   * @return {any}
   */
  getKeyByIndex(index) {
    return Object.keys( this.elements[index] )[0];
  }

  getByKey(key) {
    try {
      return this.elements[this.findIndexOfKey(key)][key];
    } catch (e) {
      console.error(e);
    }
  };

  findIndexOfKey(key, Callback) {
    for (let i = 0; i < this.elements.length; i++) {
      if (Object.keys(this.elements[i])[0] === key) {
        return i;
      }
    }
    return false;
  }
}

// Type safe list.
class List extends Collection {
  /**
   * 
   * The Type of the list.
   * ('string' | 'number' | 'int' | 'float' | 'boolean')
   * @param {String} type
   */
  constructor(type) {
    super(false, type);
  }

  /**
   * Add a new item to the List<T>.
   * @param {Type} value
   */
  add(value) {
    switch (this.type) {
      case 'int':
        if (this.isInt(value)) {
          this.push(value);
          break;
        }
      case 'float':
        if (this.isFloat(value)) {
          this.push(value);
          break;
        }
      default:
        if (typeof value === this.type && value !== 'float' && value !== 'int')
          this.push(value);
        else
          throw Errors.wrongType(this.type);
    }
  };

  /**
   * Remove an new item from the List<T> by index.
   * @param {Number} index
   */
  remove(index) {
    this.splice(index);
  };

  /**
   * (private)
   * @param {Number} value
   */
  isInt(value) {
    if (typeof value !== 'number')
      return false;

    return value % 1 === 0;
  }

  /**
   * (private)
   * @param {Number} value
   */
  isFloat(value) {
    if (typeof value !== 'number')
      return false;

    return value % 1 !== 0;
  }
}
