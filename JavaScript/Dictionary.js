//
// My implementation of a simple dictionary.
//

class Errors {
  static get existingKey() { return 'An item with the same key has already been added.' };
}

class Dictionary {
  constructor(uniqueKeys) {
    this.elements = [];

    this.uniqueKeys = uniqueKeys;
    if (!uniqueKeys) this.uniqueKeys = false;

    this.count = () => {
      this.elements.length;
    };

    this.add = (key, value) => {
      if (this.uniqueKeys && this.findIndexOfKey(key) !== undefined)
        throw new Error(Errors.existingKey);

      this.elements.push({ [key]: value });
    };

    this.remove = (key) => {
      this.elements.splice(this.findIndexOfKey(key), 1);
    };

    this.clear = () => {
      this.elements = [];
    }

    this.getByKey = (key) => {
      return this.elements[this.findIndexOfKey(key)][key];
    };

    this.findIndexOfKey = (key, Callback) => {
      for (let i = 0; i < this.elements.length; i++) {
        if (Object.keys(this.elements[i])[0] === key) {
          return i;
        }
      }
    }
  }
}
