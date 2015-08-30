/*!
 * https://github.com/es-shims/es5-shim
 * @license es5-shim Copyright 2009-2015 by contributors, MIT License
 * see https://github.com/es-shims/es5-shim/blob/master/LICENSE
 */

// vim: ts=4 sts=4 sw=4 expandtab

// Add semicolon to prevent IIFE from being passed as argument to concatenated code.
;

// UMD (Universal Module Definition)
// see https://github.com/umdjs/umd/blob/master/returnExports.js
(function (root, factory) {
    'use strict';
    /*global define, exports, module */
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define(factory);
    } else if (typeof exports === 'object') {
        // Node. Does not work with strict CommonJS, but
        // only CommonJS-like enviroments that support module.exports,
        // like Node.
        module.exports = factory();
    } else {
        // Browser globals (root is window)
        root.returnExports = factory();
    }
}(this, function () {

/**
 * Brings an environment as close to ECMAScript 5 compliance
 * as is possible with the facilities of erstwhile engines.
 *
 * Annotated ES5: http://es5.github.com/ (specific links below)
 * ES5 Spec: http://www.ecma-international.org/publications/files/ECMA-ST/Ecma-262.pdf
 * Required reading: http://javascriptweblog.wordpress.com/2011/12/05/extending-javascript-natives/
 */

// Shortcut to an often accessed properties, in order to avoid multiple
// dereference that costs universally.
var ArrayPrototype = Array.prototype;
var ObjectPrototype = Object.prototype;
var FunctionPrototype = Function.prototype;
var StringPrototype = String.prototype;
var NumberPrototype = Number.prototype;
var array_slice = ArrayPrototype.slice;
var array_splice = ArrayPrototype.splice;
var array_push = ArrayPrototype.push;
var array_unshift = ArrayPrototype.unshift;
var call = FunctionPrototype.call;

// Having a toString local variable name breaks in Opera so use to_string.
var to_string = ObjectPrototype.toString;

var isArray = Array.isArray || function isArray(obj) {
    return to_string.call(obj) === '[object Array]';
};

var hasToStringTag = typeof Symbol === 'function' && typeof Symbol.toStringTag === 'symbol';
var isCallable; /* inlined from https://npmjs.com/is-callable */ var fnToStr = Function.prototype.toString, tryFunctionObject = function tryFunctionObject(value) { try { fnToStr.call(value); return true; } catch (e) { return false; } }, fnClass = '[object Function]', genClass = '[object GeneratorFunction]'; isCallable = function isCallable(value) { if (typeof value !== 'function') { return false; } if (hasToStringTag) { return tryFunctionObject(value); } var strClass = to_string.call(value); return strClass === fnClass || strClass === genClass; };
var isRegex; /* inlined from https://npmjs.com/is-regex */ var regexExec = RegExp.prototype.exec, tryRegexExec = function tryRegexExec(value) { try { regexExec.call(value); return true; } catch (e) { return false; } }, regexClass = '[object RegExp]'; isRegex = function isRegex(value) { if (typeof value !== 'object') { return false; } return hasToStringTag ? tryRegexExec(value) : to_string.call(value) === regexClass; };
var isString; /* inlined from https://npmjs.com/is-string */ var strValue = String.prototype.valueOf, tryStringObject = function tryStringObject(value) { try { strValue.call(value); return true; } catch (e) { return false; } }, stringClass = '[object String]'; isString = function isString(value) { if (typeof value === 'string') { return true; } if (typeof value !== 'object') { return false; } return hasToStringTag ? tryStringObject(value) : to_string.call(value) === stringClass; };

var isArguments = function isArguments(value) {
    var str = to_string.call(value);
    var isArgs = str === '[object Arguments]';
    if (!isArgs) {
        isArgs = !isArray(value) &&
          value !== null &&
          typeof value === 'object' &&
          typeof value.length === 'number' &&
          value.length >= 0 &&
          isCallable(value.callee);
    }
    return isArgs;
};

/* inlined from http://npmjs.com/define-properties */
var defineProperties = (function (has) {
  var supportsDescriptors = Object.defineProperty && (function () {
      try {
          Object.defineProperty({}, 'x', {});
          return true;
      } catch (e) { /* this is ES3 */
          return false;
      }
  }());

  // Define configurable, writable and non-enumerable props
  // if they don't exist.
  var defineProperty;
  if (supportsDescriptors) {
      defineProperty = function (object, name, method, forceAssign) {
          if (!forceAssign && (name in object)) { return; }
          Object.defineProperty(object, name, {
              configurable: true,
              enumerable: false,
              writable: true,
              value: method
          });
      };
  } else {
      defineProperty = function (object, name, method, forceAssign) {
          if (!forceAssign && (name in object)) { return; }
          object[name] = method;
      };
  }
  return function defineProperties(object, map, forceAssign) {
      for (var name in map) {
          if (has.call(map, name)) {
            defineProperty(object, name, map[name], forceAssign);
          }
      }
  };
}(ObjectPrototype.hasOwnProperty));

//
// Util
// ======
//

/* replaceable with https://npmjs.com/package/es-abstract /helpers/isPrimitive */
function isPrimitive(input) {
    var type = typeof input;
    return input === null ||
        type === 'undefined' ||
        type === 'boolean' ||
        type === 'number' ||
        type === 'string';
}

var ES = {
    // ES5 9.4
    // http://es5.github.com/#x9.4
    // http://jsperf.com/to-integer
    /* replaceable with https://npmjs.com/package/es-abstract ES5.ToInteger */
    ToInteger: function ToInteger(num) {
        var n = +num;
        if (n !== n) { // isNaN
            n = 0;
        } else if (n !== 0 && n !== (1 / 0) && n !== -(1 / 0)) {
            n = (n > 0 || -1) * Math.floor(Math.abs(n));
        }
        return n;
    },

    /* replaceable with https://npmjs.com/package/es-abstract ES5.ToPrimitive */
    ToPrimitive: function ToPrimitive(input) {
        var val, valueOf, toStr;
        if (isPrimitive(input)) {
            return input;
        }
        valueOf = input.valueOf;
        if (isCallable(valueOf)) {
            val = valueOf.call(input);
            if (isPrimitive(val)) {
                return val;
            }
        }
        toStr = input.toString;
        if (isCallable(toStr)) {
            val = toStr.call(input);
            if (isPrimitive(val)) {
                return val;
            }
        }
        throw new TypeError();
    },

    // ES5 9.9
    // http://es5.github.com/#x9.9
    /* replaceable with https://npmjs.com/package/es-abstract ES5.ToObject */
    ToObject: function (o) {
        /*jshint eqnull: true */
        if (o == null) { // this matches both null and undefined
            throw new TypeError("can't convert " + o + ' to object');
        }
        return Object(o);
    },

    /* replaceable with https://npmjs.com/package/es-abstract ES5.ToUint32 */
    ToUint32: function ToUint32(x) {
        return x >>> 0;
    }
};

//
// Function
// ========
//

// ES-5 15.3.4.5
// http://es5.github.com/#x15.3.4.5

var Empty = function Empty() {};

defineProperties(FunctionPrototype, {
    bind: function bind(that) { // .length is 1
        // 1. Let Target be the this value.
        var target = this;
        // 2. If IsCallable(Target) is false, throw a TypeError exception.
        if (!isCallable(target)) {
            throw new TypeError('Function.prototype.bind called on incompatible ' + target);
        }
        // 3. Let A be a new (possibly empty) internal list of all of the
        //   argument values provided after thisArg (arg1, arg2 etc), in order.
        // XXX slicedArgs will stand in for "A" if used
        var args = array_slice.call(arguments, 1); // for normal call
        // 4. Let F be a new native ECMAScript object.
        // 11. Set the [[Prototype]] internal property of F to the standard
        //   built-in Function prototype object as specified in 15.3.3.1.
        // 12. Set the [[Call]] internal property of F as described in
        //   15.3.4.5.1.
        // 13. Set the [[Construct]] internal property of F as described in
        //   15.3.4.5.2.
        // 14. Set the [[HasInstance]] internal property of F as described in
        //   15.3.4.5.3.
        var bound;
        var binder = function () {

            if (this instanceof bound) {
                // 15.3.4.5.2 [[Construct]]
                // When the [[Construct]] internal method of a function object,
                // F that was created using the bind function is called with a
                // list of arguments ExtraArgs, the following steps are taken:
                // 1. Let target be the value of F's [[TargetFunction]]
                //   internal property.
                // 2. If target has no [[Construct]] internal method, a
                //   TypeError exception is thrown.
                // 3. Let boundArgs be the value of F's [[BoundArgs]] internal
                //   property.
                // 4. Let args be a new list containing the same values as the
                //   list boundArgs in the same order followed by the same
                //   values as the list ExtraArgs in the same order.
                // 5. Return the result of calling the [[Construct]] internal
                //   method of target providing args as the arguments.

                var result = target.apply(
                    this,
                    args.concat(array_slice.call(arguments))
                );
                if (Object(result) === result) {
                    return result;
                }
                return this;

            } else {
                // 15.3.4.5.1 [[Call]]
                // When the [[Call]] internal method of a function object, F,
                // which was created using the bind function is called with a
                // this value and a list of arguments ExtraArgs, the following
                // steps are taken:
                // 1. Let boundArgs be the value of F's [[BoundArgs]] internal
                //   property.
                // 2. Let boundThis be the value of F's [[BoundThis]] internal
                //   property.
                // 3. Let target be the value of F's [[TargetFunction]] internal
                //   property.
                // 4. Let args be a new list containing the same values as the
                //   list boundArgs in the same order followed by the same
                //   values as the list ExtraArgs in the same order.
                // 5. Return the result of calling the [[Call]] internal method
                //   of target providing boundThis as the this value and
                //   providing args as the arguments.

                // equiv: target.call(this, ...boundArgs, ...args)
                return target.apply(
                    that,
                    args.concat(array_slice.call(arguments))
                );

            }

        };

        // 15. If the [[Class]] internal property of Target is "Function", then
        //     a. Let L be the length property of Target minus the length of A.
        //     b. Set the length own property of F to either 0 or L, whichever is
        //       larger.
        // 16. Else set the length own property of F to 0.

        var boundLength = Math.max(0, target.length - args.length);

        // 17. Set the attributes of the length own property of F to the values
        //   specified in 15.3.5.1.
        var boundArgs = [];
        for (var i = 0; i < boundLength; i++) {
            boundArgs.push('$' + i);
        }

        // XXX Build a dynamic function with desired amount of arguments is the only
        // way to set the length property of a function.
        // In environments where Content Security Policies enabled (Chrome extensions,
        // for ex.) all use of eval or Function costructor throws an exception.
        // However in all of these environments Function.prototype.bind exists
        // and so this code will never be executed.
        bound = Function('binder', 'return function (' + boundArgs.join(',') + '){ return binder.apply(this, arguments); }')(binder);

        if (target.prototype) {
            Empty.prototype = target.prototype;
            bound.prototype = new Empty();
            // Clean up dangling references.
            Empty.prototype = null;
        }

        // TODO
        // 18. Set the [[Extensible]] internal property of F to true.

        // TODO
        // 19. Let thrower be the [[ThrowTypeError]] function Object (13.2.3).
        // 20. Call the [[DefineOwnProperty]] internal method of F with
        //   arguments "caller", PropertyDescriptor {[[Get]]: thrower, [[Set]]:
        //   thrower, [[Enumerable]]: false, [[Configurable]]: false}, and
        //   false.
        // 21. Call the [[DefineOwnProperty]] internal method of F with
        //   arguments "arguments", PropertyDescriptor {[[Get]]: thrower,
        //   [[Set]]: thrower, [[Enumerable]]: false, [[Configurable]]: false},
        //   and false.

        // TODO
        // NOTE Function objects created using Function.prototype.bind do not
        // have a prototype property or the [[Code]], [[FormalParameters]], and
        // [[Scope]] internal properties.
        // XXX can't delete prototype in pure-js.

        // 22. Return F.
        return bound;
    }
});

// _Please note: Shortcuts are defined after `Function.prototype.bind` as we
// us it in defining shortcuts.
var owns = call.bind(ObjectPrototype.hasOwnProperty);

//
// Array
// =====
//

// ES5 15.4.4.12
// http://es5.github.com/#x15.4.4.12
var spliceNoopReturnsEmptyArray = (function () {
    var a = [1, 2];
    var result = a.splice();
    return a.length === 2 && isArray(result) && result.length === 0;
}());
defineProperties(ArrayPrototype, {
    // Safari 5.0 bug where .splice() returns undefined
    splice: function splice(start, deleteCount) {
        if (arguments.length === 0) {
            return [];
        } else {
            return array_splice.apply(this, arguments);
        }
    }
}, !spliceNoopReturnsEmptyArray);

var spliceWorksWithEmptyObject = (function () {
    var obj = {};
    ArrayPrototype.splice.call(obj, 0, 0, 1);
    return obj.length === 1;
}());
defineProperties(ArrayPrototype, {
    splice: function splice(start, deleteCount) {
        if (arguments.length === 0) { return []; }
        var args = arguments;
        this.length = Math.max(ES.ToInteger(this.length), 0);
        if (arguments.length > 0 && typeof deleteCount !== 'number') {
            args = array_slice.call(arguments);
            if (args.length < 2) {
                args.push(this.length - start);
            } else {
                args[1] = ES.ToInteger(deleteCount);
            }
        }
        return array_splice.apply(this, args);
    }
}, !spliceWorksWithEmptyObject);

// ES5 15.4.4.12
// http://es5.github.com/#x15.4.4.13
// Return len+argCount.
// [bugfix, ielt8]
// IE < 8 bug: [].unshift(0) === undefined but should be "1"
var hasUnshiftReturnValueBug = [].unshift(0) !== 1;
defineProperties(ArrayPrototype, {
    unshift: function () {
        array_unshift.apply(this, arguments);
        return this.length;
    }
}, hasUnshiftReturnValueBug);

// ES5 15.4.3.2
// http://es5.github.com/#x15.4.3.2
// https://developer.mozilla.org/en/JavaScript/Reference/Global_Objects/Array/isArray
defineProperties(Array, { isArray: isArray });

// The IsCallable() check in the Array functions
// has been replaced with a strict check on the
// internal class of the object to trap cases where
// the provided function was actually a regular
// expression literal, which in V8 and
// JavaScriptCore is a typeof "function".  Only in
// V8 are regular expression literals permitted as
// reduce parameters, so it is desirable in the
// general case for the shim to match the more
// strict and common behavior of rejecting regular
// expressions.

// ES5 15.4.4.18
// http://es5.github.com/#x15.4.4.18
// https://developer.mozilla.org/en/JavaScript/Reference/Global_Objects/array/forEach

// Check failure of by-index access of string characters (IE < 9)
// and failure of `0 in boxedString` (Rhino)
var boxedString = Object('a');
var splitString = boxedString[0] !== 'a' || !(0 in boxedString);

var properlyBoxesContext = function properlyBoxed(method) {
    // Check node 0.6.21 bug where third parameter is not boxed
    var properlyBoxesNonStrict = true;
    var properlyBoxesStrict = true;
    if (method) {
        method.call('foo', function (_, __, context) {
            if (typeof context !== 'object') { properlyBoxesNonStrict = false; }
        });

        method.call([1], function () {
            'use strict';
            properlyBoxesStrict = typeof this === 'string';
        }, 'x');
    }
    return !!method && properlyBoxesNonStrict && properlyBoxesStrict;
};

defineProperties(ArrayPrototype, {
    forEach: function forEach(fun /*, thisp*/) {
        var object = ES.ToObject(this),
            self = splitString && isString(this) ? this.split('') : object,
            thisp = arguments[1],
            i = -1,
            length = self.length >>> 0;

        // If no callback function or if callback is not a callable function
        if (!isCallable(fun)) {
            throw new TypeError(); // TODO message
        }

        while (++i < length) {
            if (i in self) {
                // Invoke the callback function with call, passing arguments:
                // context, property value, property key, thisArg object
                // context
                fun.call(thisp, self[i], i, object);
            }
        }
    }
}, !properlyBoxesContext(ArrayPrototype.forEach));

// ES5 15.4.4.19
// http://es5.github.com/#x15.4.4.19
// https://developer.mozilla.org/en/Core_JavaScript_1.5_Reference/Objects/Array/map
defineProperties(ArrayPrototype, {
    map: function map(fun /*, thisp*/) {
        var object = ES.ToObject(this),
            self = splitString && isString(this) ? this.split('') : object,
            length = self.length >>> 0,
            result = Array(length),
            thisp = arguments[1];

        // If no callback function or if callback is not a callable function
        if (!isCallable(fun)) {
            throw new TypeError(fun + ' is not a function');
        }

        for (var i = 0; i < length; i++) {
            if (i in self) {
                result[i] = fun.call(thisp, self[i], i, object);
            }
        }
        return result;
    }
}, !properlyBoxesContext(ArrayPrototype.map));

// ES5 15.4.4.20
// http://es5.github.com/#x15.4.4.20
// https://developer.mozilla.org/en/Core_JavaScript_1.5_Reference/Objects/Array/filter
defineProperties(ArrayPrototype, {
    filter: function filter(fun /*, thisp */) {
        var object = ES.ToObject(this),
            self = splitString && isString(this) ? this.split('') : object,
            length = self.length >>> 0,
            result = [],
            value,
            thisp = arguments[1];

        // If no callback function or if callback is not a callable function
        if (!isCallable(fun)) {
            throw new TypeError(fun + ' is not a function');
        }

        for (var i = 0; i < length; i++) {
            if (i in self) {
                value = self[i];
                if (fun.call(thisp, value, i, object)) {
                    result.push(value);
                }
            }
        }
        return result;
    }
}, !properlyBoxesContext(ArrayPrototype.filter));

// ES5 15.4.4.16
// http://es5.github.com/#x15.4.4.16
// https://developer.mozilla.org/en/JavaScript/Reference/Global_Objects/Array/every
defineProperties(ArrayPrototype, {
    every: function every(fun /*, thisp */) {
        var object = ES.ToObject(this),
            self = splitString && isString(this) ? this.split('') : object,
            length = self.length >>> 0,
            thisp = arguments[1];

        // If no callback function or if callback is not a callable function
        if (!isCallable(fun)) {
            throw new TypeError(fun + ' is not a function');
        }

        for (var i = 0; i < length; i++) {
            if (i in self && !fun.call(thisp, self[i], i, object)) {
                return false;
            }
        }
        return true;
    }
}, !properlyBoxesContext(ArrayPrototype.every));

// ES5 15.4.4.17
// http://es5.github.com/#x15.4.4.17
// https://developer.mozilla.org/en/JavaScript/Reference/Global_Objects/Array/some
defineProperties(ArrayPrototype, {
    some: function some(fun /*, thisp */) {
        var object = ES.ToObject(this),
            self = splitString && isString(this) ? this.split('') : object,
            length = self.length >>> 0,
            thisp = arguments[1];

        // If no callback function or if callback is not a callable function
        if (!isCallable(fun)) {
            throw new TypeError(fun + ' is not a function');
        }

        for (var i = 0; i < length; i++) {
            if (i in self && fun.call(thisp, self[i], i, object)) {
                return true;
            }
        }
        return false;
    }
}, !properlyBoxesContext(ArrayPrototype.some));

// ES5 15.4.4.21
// http://es5.github.com/#x15.4.4.21
// https://developer.mozilla.org/en/Core_JavaScript_1.5_Reference/Objects/Array/reduce
var reduceCoercesToObject = false;
if (ArrayPrototype.reduce) {
    reduceCoercesToObject = typeof ArrayPrototype.reduce.call('es5', function (_, __, ___, list) { return list; }) === 'object';
}
defineProperties(ArrayPrototype, {
    reduce: function reduce(fun /*, initial*/) {
        var object = ES.ToObject(this),
            self = splitString && isString(this) ? this.split('') : object,
            length = self.length >>> 0;

        // If no callback function or if callback is not a callable function
        if (!isCallable(fun)) {
            throw new TypeError(fun + ' is not a function');
        }

        // no value to return if no initial value and an empty array
        if (!length && arguments.length === 1) {
            throw new TypeError('reduce of empty array with no initial value');
        }

        var i = 0;
        var result;
        if (arguments.length >= 2) {
            result = arguments[1];
        } else {
            do {
                if (i in self) {
                    result = self[i++];
                    break;
                }

                // if array contains no values, no initial value to return
                if (++i >= length) {
                    throw new TypeError('reduce of empty array with no initial value');
                }
            } while (true);
        }

        for (; i < length; i++) {
            if (i in self) {
                result = fun.call(void 0, result, self[i], i, object);
            }
        }

        return result;
    }
}, !reduceCoercesToObject);

// ES5 15.4.4.22
// http://es5.github.com/#x15.4.4.22
// https://developer.mozilla.org/en/Core_JavaScript_1.5_Reference/Objects/Array/reduceRight
var reduceRightCoercesToObject = false;
if (ArrayPrototype.reduceRight) {
    reduceRightCoercesToObject = typeof ArrayPrototype.reduceRight.call('es5', function (_, __, ___, list) { return list; }) === 'object';
}
defineProperties(ArrayPrototype, {
    reduceRight: function reduceRight(fun /*, initial*/) {
        var object = ES.ToObject(this),
            self = splitString && isString(this) ? this.split('') : object,
            length = self.length >>> 0;

        // If no callback function or if callback is not a callable function
        if (!isCallable(fun)) {
            throw new TypeError(fun + ' is not a function');
        }

        // no value to return if no initial value, empty array
        if (!length && arguments.length === 1) {
            throw new TypeError('reduceRight of empty array with no initial value');
        }

        var result, i = length - 1;
        if (arguments.length >= 2) {
            result = arguments[1];
        } else {
            do {
                if (i in self) {
                    result = self[i--];
                    break;
                }

                // if array contains no values, no initial value to return
                if (--i < 0) {
                    throw new TypeError('reduceRight of empty array with no initial value');
                }
            } while (true);
        }

        if (i < 0) {
            return result;
        }

        do {
            if (i in self) {
                result = fun.call(void 0, result, self[i], i, object);
            }
        } while (i--);

        return result;
    }
}, !reduceRightCoercesToObject);

// ES5 15.4.4.14
// http://es5.github.com/#x15.4.4.14
// https://developer.mozilla.org/en/JavaScript/Reference/Global_Objects/Array/indexOf
var hasFirefox2IndexOfBug = Array.prototype.indexOf && [0, 1].indexOf(1, 2) !== -1;
defineProperties(ArrayPrototype, {
    indexOf: function indexOf(sought /*, fromIndex */) {
        var self = splitString && isString(this) ? this.split('') : ES.ToObject(this),
            length = self.length >>> 0;

        if (!length) {
            return -1;
        }

        var i = 0;
        if (arguments.length > 1) {
            i = ES.ToInteger(arguments[1]);
        }

        // handle negative indices
        i = i >= 0 ? i : Math.max(0, length + i);
        for (; i < length; i++) {
            if (i in self && self[i] === sought) {
                return i;
            }
        }
        return -1;
    }
}, hasFirefox2IndexOfBug);

// ES5 15.4.4.15
// http://es5.github.com/#x15.4.4.15
// https://developer.mozilla.org/en/JavaScript/Reference/Global_Objects/Array/lastIndexOf
var hasFirefox2LastIndexOfBug = Array.prototype.lastIndexOf && [0, 1].lastIndexOf(0, -3) !== -1;
defineProperties(ArrayPrototype, {
    lastIndexOf: function lastIndexOf(sought /*, fromIndex */) {
        var self = splitString && isString(this) ? this.split('') : ES.ToObject(this),
            length = self.length >>> 0;

        if (!length) {
            return -1;
        }
        var i = length - 1;
        if (arguments.length > 1) {
            i = Math.min(i, ES.ToInteger(arguments[1]));
        }
        // handle negative indices
        i = i >= 0 ? i : length - Math.abs(i);
        for (; i >= 0; i--) {
            if (i in self && sought === self[i]) {
                return i;
            }
        }
        return -1;
    }
}, hasFirefox2LastIndexOfBug);

//
// Object
// ======
//

// ES5 15.2.3.14
// http://es5.github.com/#x15.2.3.14

// http://whattheheadsaid.com/2010/10/a-safer-object-keys-compatibility-implementation
var hasDontEnumBug = !({'toString': null}).propertyIsEnumerable('toString'),
    hasProtoEnumBug = function () {}.propertyIsEnumerable('prototype'),
    hasStringEnumBug = !owns('x', '0'),
    dontEnums = [
        'toString',
        'toLocaleString',
        'valueOf',
        'hasOwnProperty',
        'isPrototypeOf',
        'propertyIsEnumerable',
        'constructor'
    ],
    dontEnumsLength = dontEnums.length;

defineProperties(Object, {
    keys: function keys(object) {
        var isFn = isCallable(object),
            isArgs = isArguments(object),
            isObject = object !== null && typeof object === 'object',
            isStr = isObject && isString(object);

        if (!isObject && !isFn && !isArgs) {
            throw new TypeError('Object.keys called on a non-object');
        }

        var theKeys = [];
        var skipProto = hasProtoEnumBug && isFn;
        if ((isStr && hasStringEnumBug) || isArgs) {
            for (var i = 0; i < object.length; ++i) {
                theKeys.push(String(i));
            }
        }

        if (!isArgs) {
            for (var name in object) {
                if (!(skipProto && name === 'prototype') && owns(object, name)) {
                    theKeys.push(String(name));
                }
            }
        }

        if (hasDontEnumBug) {
            var ctor = object.constructor,
                skipConstructor = ctor && ctor.prototype === object;
            for (var j = 0; j < dontEnumsLength; j++) {
                var dontEnum = dontEnums[j];
                if (!(skipConstructor && dontEnum === 'constructor') && owns(object, dontEnum)) {
                    theKeys.push(dontEnum);
                }
            }
        }
        return theKeys;
    }
});

var keysWorksWithArguments = Object.keys && (function () {
    // Safari 5.0 bug
    return Object.keys(arguments).length === 2;
}(1, 2));
var originalKeys = Object.keys;
defineProperties(Object, {
    keys: function keys(object) {
        if (isArguments(object)) {
            return originalKeys(ArrayPrototype.slice.call(object));
        } else {
            return originalKeys(object);
        }
    }
}, !keysWorksWithArguments);

//
// Date
// ====
//

// ES5 15.9.5.43
// http://es5.github.com/#x15.9.5.43
// This function returns a String value represent the instance in time
// represented by this Date object. The format of the String is the Date Time
// string format defined in 15.9.1.15. All fields are present in the String.
// The time zone is always UTC, denoted by the suffix Z. If the time value of
// this object is not a finite Number a RangeError exception is thrown.
var negativeDate = -62198755200000;
var negativeYearString = '-000001';
var hasNegativeDateBug = Date.prototype.toISOString && new Date(negativeDate).toISOString().indexOf(negativeYearString) === -1;

defineProperties(Date.prototype, {
    toISOString: function toISOString() {
        var result, length, value, year, month;
        if (!isFinite(this)) {
            throw new RangeError('Date.prototype.toISOString called on non-finite value.');
        }

        year = this.getUTCFullYear();

        month = this.getUTCMonth();
        // see https://github.com/es-shims/es5-shim/issues/111
        year += Math.floor(month / 12);
        month = (month % 12 + 12) % 12;

        // the date time string format is specified in 15.9.1.15.
        result = [month + 1, this.getUTCDate(), this.getUTCHours(), this.getUTCMinutes(), this.getUTCSeconds()];
        year = (
            (year < 0 ? '-' : (year > 9999 ? '+' : '')) +
            ('00000' + Math.abs(year)).slice((0 <= year && year <= 9999) ? -4 : -6)
        );

        length = result.length;
        while (length--) {
            value = result[length];
            // pad months, days, hours, minutes, and seconds to have two
            // digits.
            if (value < 10) {
                result[length] = '0' + value;
            }
        }
        // pad milliseconds to have three digits.
        return (
            year + '-' + result.slice(0, 2).join('-') +
            'T' + result.slice(2).join(':') + '.' +
            ('000' + this.getUTCMilliseconds()).slice(-3) + 'Z'
        );
    }
}, hasNegativeDateBug);

// ES5 15.9.5.44
// http://es5.github.com/#x15.9.5.44
// This function provides a String representation of a Date object for use by
// JSON.stringify (15.12.3).
var dateToJSONIsSupported = (function () {
    try {
        return Date.prototype.toJSON &&
            new Date(NaN).toJSON() === null &&
            new Date(negativeDate).toJSON().indexOf(negativeYearString) !== -1 &&
            Date.prototype.toJSON.call({ // generic
                toISOString: function () { return true; }
            });
    } catch (e) {
        return false;
    }
}());
if (!dateToJSONIsSupported) {
    Date.prototype.toJSON = function toJSON(key) {
        // When the toJSON method is called with argument key, the following
        // steps are taken:

        // 1.  Let O be the result of calling ToObject, giving it the this
        // value as its argument.
        // 2. Let tv be ES.ToPrimitive(O, hint Number).
        var O = Object(this);
        var tv = ES.ToPrimitive(O);
        // 3. If tv is a Number and is not finite, return null.
        if (typeof tv === 'number' && !isFinite(tv)) {
            return null;
        }
        // 4. Let toISO be the result of calling the [[Get]] internal method of
        // O with argument "toISOString".
        var toISO = O.toISOString;
        // 5. If IsCallable(toISO) is false, throw a TypeError exception.
        if (!isCallable(toISO)) {
            throw new TypeError('toISOString property is not callable');
        }
        // 6. Return the result of calling the [[Call]] internal method of
        //  toISO with O as the this value and an empty argument list.
        return toISO.call(O);

        // NOTE 1 The argument is ignored.

        // NOTE 2 The toJSON function is intentionally generic; it does not
        // require that its this value be a Date object. Therefore, it can be
        // transferred to other kinds of objects for use as a method. However,
        // it does require that any such object have a toISOString method. An
        // object is free to use the argument key to filter its
        // stringification.
    };
}

// ES5 15.9.4.2
// http://es5.github.com/#x15.9.4.2
// based on work shared by Daniel Friesen (dantman)
// http://gist.github.com/303249
var supportsExtendedYears = Date.parse('+033658-09-27T01:46:40.000Z') === 1e15;
var acceptsInvalidDates = !isNaN(Date.parse('2012-04-04T24:00:00.500Z')) || !isNaN(Date.parse('2012-11-31T23:59:59.000Z'));
var doesNotParseY2KNewYear = isNaN(Date.parse('2000-01-01T00:00:00.000Z'));
if (!Date.parse || doesNotParseY2KNewYear || acceptsInvalidDates || !supportsExtendedYears) {
    // XXX global assignment won't work in embeddings that use
    // an alternate object for the context.
    /*global Date: true */
    /*eslint-disable no-undef*/
    Date = (function (NativeDate) {
    /*eslint-enable no-undef*/
        // Date.length === 7
        function Date(Y, M, D, h, m, s, ms) {
            var length = arguments.length;
            if (this instanceof NativeDate) {
                var date = length === 1 && String(Y) === Y ? // isString(Y)
                    // We explicitly pass it through parse:
                    new NativeDate(Date.parse(Y)) :
                    // We have to manually make calls depending on argument
                    // length here
                    length >= 7 ? new NativeDate(Y, M, D, h, m, s, ms) :
                    length >= 6 ? new NativeDate(Y, M, D, h, m, s) :
                    length >= 5 ? new NativeDate(Y, M, D, h, m) :
                    length >= 4 ? new NativeDate(Y, M, D, h) :
                    length >= 3 ? new NativeDate(Y, M, D) :
                    length >= 2 ? new NativeDate(Y, M) :
                    length >= 1 ? new NativeDate(Y) :
                                  new NativeDate();
                // Prevent mixups with unfixed Date object
                defineProperties(date, { constructor: Date }, true);
                return date;
            }
            return NativeDate.apply(this, arguments);
        }

        // 15.9.1.15 Date Time String Format.
        var isoDateExpression = new RegExp('^' +
            '(\\d{4}|[+-]\\d{6})' + // four-digit year capture or sign +
                                      // 6-digit extended year
            '(?:-(\\d{2})' + // optional month capture
            '(?:-(\\d{2})' + // optional day capture
            '(?:' + // capture hours:minutes:seconds.milliseconds
                'T(\\d{2})' + // hours capture
                ':(\\d{2})' + // minutes capture
                '(?:' + // optional :seconds.milliseconds
                    ':(\\d{2})' + // seconds capture
                    '(?:(\\.\\d{1,}))?' + // milliseconds capture
                ')?' +
            '(' + // capture UTC offset component
                'Z|' + // UTC capture
                '(?:' + // offset specifier +/-hours:minutes
                    '([-+])' + // sign capture
                    '(\\d{2})' + // hours offset capture
                    ':(\\d{2})' + // minutes offset capture
                ')' +
            ')?)?)?)?' +
        '$');

        var months = [
            0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365
        ];

        function dayFromMonth(year, month) {
            var t = month > 1 ? 1 : 0;
            return (
                months[month] +
                Math.floor((year - 1969 + t) / 4) -
                Math.floor((year - 1901 + t) / 100) +
                Math.floor((year - 1601 + t) / 400) +
                365 * (year - 1970)
            );
        }

        function toUTC(t) {
            return Number(new NativeDate(1970, 0, 1, 0, 0, 0, t));
        }

        // Copy any custom methods a 3rd party library may have added
        for (var key in NativeDate) {
            Date[key] = NativeDate[key];
        }

        // Copy "native" methods explicitly; they may be non-enumerable
        Date.now = NativeDate.now;
        Date.UTC = NativeDate.UTC;
        Date.prototype = NativeDate.prototype;
        Date.prototype.constructor = Date;

        // Upgrade Date.parse to handle simplified ISO 8601 strings
        Date.parse = function parse(string) {
            var match = isoDateExpression.exec(string);
            if (match) {
                // parse months, days, hours, minutes, seconds, and milliseconds
                // provide default values if necessary
                // parse the UTC offset component
                var year = Number(match[1]),
                    month = Number(match[2] || 1) - 1,
                    day = Number(match[3] || 1) - 1,
                    hour = Number(match[4] || 0),
                    minute = Number(match[5] || 0),
                    second = Number(match[6] || 0),
                    millisecond = Math.floor(Number(match[7] || 0) * 1000),
                    // When time zone is missed, local offset should be used
                    // (ES 5.1 bug)
                    // see https://bugs.ecmascript.org/show_bug.cgi?id=112
                    isLocalTime = Boolean(match[4] && !match[8]),
                    signOffset = match[9] === '-' ? 1 : -1,
                    hourOffset = Number(match[10] || 0),
                    minuteOffset = Number(match[11] || 0),
                    result;
                if (
                    hour < (
                        minute > 0 || second > 0 || millisecond > 0 ?
                        24 : 25
                    ) &&
                    minute < 60 && second < 60 && millisecond < 1000 &&
                    month > -1 && month < 12 && hourOffset < 24 &&
                    minuteOffset < 60 && // detect invalid offsets
                    day > -1 &&
                    day < (
                        dayFromMonth(year, month + 1) -
                        dayFromMonth(year, month)
                    )
                ) {
                    result = (
                        (dayFromMonth(year, month) + day) * 24 +
                        hour +
                        hourOffset * signOffset
                    ) * 60;
                    result = (
                        (result + minute + minuteOffset * signOffset) * 60 +
                        second
                    ) * 1000 + millisecond;
                    if (isLocalTime) {
                        result = toUTC(result);
                    }
                    if (-8.64e15 <= result && result <= 8.64e15) {
                        return result;
                    }
                }
                return NaN;
            }
            return NativeDate.parse.apply(this, arguments);
        };

        return Date;
    }(Date));
    /*global Date: false */
}

// ES5 15.9.4.4
// http://es5.github.com/#x15.9.4.4
if (!Date.now) {
    Date.now = function now() {
        return new Date().getTime();
    };
}

//
// Number
// ======
//

// ES5.1 15.7.4.5
// http://es5.github.com/#x15.7.4.5
var hasToFixedBugs = NumberPrototype.toFixed && (
  (0.00008).toFixed(3) !== '0.000' ||
  (0.9).toFixed(0) !== '1' ||
  (1.255).toFixed(2) !== '1.25' ||
  (1000000000000000128).toFixed(0) !== '1000000000000000128'
);

var toFixedHelpers = {
  base: 1e7,
  size: 6,
  data: [0, 0, 0, 0, 0, 0],
  multiply: function multiply(n, c) {
      var i = -1;
      var c2 = c;
      while (++i < toFixedHelpers.size) {
          c2 += n * toFixedHelpers.data[i];
          toFixedHelpers.data[i] = c2 % toFixedHelpers.base;
          c2 = Math.floor(c2 / toFixedHelpers.base);
      }
  },
  divide: function divide(n) {
      var i = toFixedHelpers.size, c = 0;
      while (--i >= 0) {
          c += toFixedHelpers.data[i];
          toFixedHelpers.data[i] = Math.floor(c / n);
          c = (c % n) * toFixedHelpers.base;
      }
  },
  numToString: function numToString() {
      var i = toFixedHelpers.size;
      var s = '';
      while (--i >= 0) {
          if (s !== '' || i === 0 || toFixedHelpers.data[i] !== 0) {
              var t = String(toFixedHelpers.data[i]);
              if (s === '') {
                  s = t;
              } else {
                  s += '0000000'.slice(0, 7 - t.length) + t;
              }
          }
      }
      return s;
  },
  pow: function pow(x, n, acc) {
      return (n === 0 ? acc : (n % 2 === 1 ? pow(x, n - 1, acc * x) : pow(x * x, n / 2, acc)));
  },
  log: function log(x) {
      var n = 0;
      var x2 = x;
      while (x2 >= 4096) {
          n += 12;
          x2 /= 4096;
      }
      while (x2 >= 2) {
          n += 1;
          x2 /= 2;
      }
      return n;
  }
};

defineProperties(NumberPrototype, {
    toFixed: function toFixed(fractionDigits) {
        var f, x, s, m, e, z, j, k;

        // Test for NaN and round fractionDigits down
        f = Number(fractionDigits);
        f = f !== f ? 0 : Math.floor(f);

        if (f < 0 || f > 20) {
            throw new RangeError('Number.toFixed called with invalid number of decimals');
        }

        x = Number(this);

        // Test for NaN
        if (x !== x) {
            return 'NaN';
        }

        // If it is too big or small, return the string value of the number
        if (x <= -1e21 || x >= 1e21) {
            return String(x);
        }

        s = '';

        if (x < 0) {
            s = '-';
            x = -x;
        }

        m = '0';

        if (x > 1e-21) {
            // 1e-21 < x < 1e21
            // -70 < log2(x) < 70
            e = toFixedHelpers.log(x * toFixedHelpers.pow(2, 69, 1)) - 69;
            z = (e < 0 ? x * toFixedHelpers.pow(2, -e, 1) : x / toFixedHelpers.pow(2, e, 1));
            z *= 0x10000000000000; // Math.pow(2, 52);
            e = 52 - e;

            // -18 < e < 122
            // x = z / 2 ^ e
            if (e > 0) {
                toFixedHelpers.multiply(0, z);
                j = f;

                while (j >= 7) {
                    toFixedHelpers.multiply(1e7, 0);
                    j -= 7;
                }

                toFixedHelpers.multiply(toFixedHelpers.pow(10, j, 1), 0);
                j = e - 1;

                while (j >= 23) {
                    toFixedHelpers.divide(1 << 23);
                    j -= 23;
                }

                toFixedHelpers.divide(1 << j);
                toFixedHelpers.multiply(1, 1);
                toFixedHelpers.divide(2);
                m = toFixedHelpers.numToString();
            } else {
                toFixedHelpers.multiply(0, z);
                toFixedHelpers.multiply(1 << (-e), 0);
                m = toFixedHelpers.numToString() + '0.00000000000000000000'.slice(2, 2 + f);
            }
        }

        if (f > 0) {
            k = m.length;

            if (k <= f) {
                m = s + '0.0000000000000000000'.slice(0, f - k + 2) + m;
            } else {
                m = s + m.slice(0, k - f) + '.' + m.slice(k - f);
            }
        } else {
            m = s + m;
        }

        return m;
    }
}, hasToFixedBugs);

//
// String
// ======
//

// ES5 15.5.4.14
// http://es5.github.com/#x15.5.4.14

// [bugfix, IE lt 9, firefox 4, Konqueror, Opera, obscure browsers]
// Many browsers do not split properly with regular expressions or they
// do not perform the split correctly under obscure conditions.
// See http://blog.stevenlevithan.com/archives/cross-browser-split
// I've tested in many browsers and this seems to cover the deviant ones:
//    'ab'.split(/(?:ab)*/) should be ["", ""], not [""]
//    '.'.split(/(.?)(.?)/) should be ["", ".", "", ""], not ["", ""]
//    'tesst'.split(/(s)*/) should be ["t", undefined, "e", "s", "t"], not
//       [undefined, "t", undefined, "e", ...]
//    ''.split(/.?/) should be [], not [""]
//    '.'.split(/()()/) should be ["."], not ["", "", "."]

var string_split = StringPrototype.split;
if (
    'ab'.split(/(?:ab)*/).length !== 2 ||
    '.'.split(/(.?)(.?)/).length !== 4 ||
    'tesst'.split(/(s)*/)[1] === 't' ||
    'test'.split(/(?:)/, -1).length !== 4 ||
    ''.split(/.?/).length ||
    '.'.split(/()()/).length > 1
) {
    (function () {
        var compliantExecNpcg = typeof (/()??/).exec('')[1] === 'undefined'; // NPCG: nonparticipating capturing group

        StringPrototype.split = function (separator, limit) {
            var string = this;
            if (typeof separator === 'undefined' && limit === 0) {
                return [];
            }

            // If `separator` is not a regex, use native split
            if (!isRegex(separator)) {
                return string_split.call(this, separator, limit);
            }

            var output = [];
            var flags = (separator.ignoreCase ? 'i' : '') +
                        (separator.multiline ? 'm' : '') +
                        (separator.extended ? 'x' : '') + // Proposed for ES6
                        (separator.sticky ? 'y' : ''), // Firefox 3+
                lastLastIndex = 0,
                // Make `global` and avoid `lastIndex` issues by working with a copy
                separator2, match, lastIndex, lastLength;
            var separatorCopy = new RegExp(separator.source, flags + 'g');
            string += ''; // Type-convert
            if (!compliantExecNpcg) {
                // Doesn't need flags gy, but they don't hurt
                separator2 = new RegExp('^' + separatorCopy.source + '$(?!\\s)', flags);
            }
            /* Values for `limit`, per the spec:
             * If undefined: 4294967295 // Math.pow(2, 32) - 1
             * If 0, Infinity, or NaN: 0
             * If positive number: limit = Math.floor(limit); if (limit > 4294967295) limit -= 4294967296;
             * If negative number: 4294967296 - Math.floor(Math.abs(limit))
             * If other: Type-convert, then use the above rules
             */
            var splitLimit = typeof limit === 'undefined' ?
                -1 >>> 0 : // Math.pow(2, 32) - 1
                ES.ToUint32(limit);
            match = separatorCopy.exec(string);
            while (match) {
                // `separatorCopy.lastIndex` is not reliable cross-browser
                lastIndex = match.index + match[0].length;
                if (lastIndex > lastLastIndex) {
                    output.push(string.slice(lastLastIndex, match.index));
                    // Fix browsers whose `exec` methods don't consistently return `undefined` for
                    // nonparticipating capturing groups
                    if (!compliantExecNpcg && match.length > 1) {
                        /*eslint-disable no-loop-func */
                        match[0].replace(separator2, function () {
                            for (var i = 1; i < arguments.length - 2; i++) {
                                if (typeof arguments[i] === 'undefined') {
                                    match[i] = void 0;
                                }
                            }
                        });
                        /*eslint-enable no-loop-func */
                    }
                    if (match.length > 1 && match.index < string.length) {
                        array_push.apply(output, match.slice(1));
                    }
                    lastLength = match[0].length;
                    lastLastIndex = lastIndex;
                    if (output.length >= splitLimit) {
                        break;
                    }
                }
                if (separatorCopy.lastIndex === match.index) {
                    separatorCopy.lastIndex++; // Avoid an infinite loop
                }
                match = separatorCopy.exec(string);
            }
            if (lastLastIndex === string.length) {
                if (lastLength || !separatorCopy.test('')) {
                    output.push('');
                }
            } else {
                output.push(string.slice(lastLastIndex));
            }
            return output.length > splitLimit ? output.slice(0, splitLimit) : output;
        };
    }());

// [bugfix, chrome]
// If separator is undefined, then the result array contains just one String,
// which is the this value (converted to a String). If limit is not undefined,
// then the output array is truncated so that it contains no more than limit
// elements.
// "0".split(undefined, 0) -> []
} else if ('0'.split(void 0, 0).length) {
    StringPrototype.split = function split(separator, limit) {
        if (typeof separator === 'undefined' && limit === 0) { return []; }
        return string_split.call(this, separator, limit);
    };
}

var str_replace = StringPrototype.replace;
var replaceReportsGroupsCorrectly = (function () {
    var groups = [];
    'x'.replace(/x(.)?/g, function (match, group) {
        groups.push(group);
    });
    return groups.length === 1 && typeof groups[0] === 'undefined';
}());

if (!replaceReportsGroupsCorrectly) {
    StringPrototype.replace = function replace(searchValue, replaceValue) {
        var isFn = isCallable(replaceValue);
        var hasCapturingGroups = isRegex(searchValue) && (/\)[*?]/).test(searchValue.source);
        if (!isFn || !hasCapturingGroups) {
            return str_replace.call(this, searchValue, replaceValue);
        } else {
            var wrappedReplaceValue = function (match) {
                var length = arguments.length;
                var originalLastIndex = searchValue.lastIndex;
                searchValue.lastIndex = 0;
                var args = searchValue.exec(match) || [];
                searchValue.lastIndex = originalLastIndex;
                args.push(arguments[length - 2], arguments[length - 1]);
                return replaceValue.apply(this, args);
            };
            return str_replace.call(this, searchValue, wrappedReplaceValue);
        }
    };
}

// ECMA-262, 3rd B.2.3
// Not an ECMAScript standard, although ECMAScript 3rd Edition has a
// non-normative section suggesting uniform semantics and it should be
// normalized across all browsers
// [bugfix, IE lt 9] IE < 9 substr() with negative value not working in IE
var string_substr = StringPrototype.substr;
var hasNegativeSubstrBug = ''.substr && '0b'.substr(-1) !== 'b';
defineProperties(StringPrototype, {
    substr: function substr(start, length) {
        var normalizedStart = start;
        if (start < 0) {
            normalizedStart = Math.max(this.length + start, 0);
        }
        return string_substr.call(this, normalizedStart, length);
    }
}, hasNegativeSubstrBug);

// ES5 15.5.4.20
// whitespace from: http://es5.github.io/#x15.5.4.20
var ws = '\x09\x0A\x0B\x0C\x0D\x20\xA0\u1680\u180E\u2000\u2001\u2002\u2003' +
    '\u2004\u2005\u2006\u2007\u2008\u2009\u200A\u202F\u205F\u3000\u2028' +
    '\u2029\uFEFF';
var zeroWidth = '\u200b';
var wsRegexChars = '[' + ws + ']';
var trimBeginRegexp = new RegExp('^' + wsRegexChars + wsRegexChars + '*');
var trimEndRegexp = new RegExp(wsRegexChars + wsRegexChars + '*$');
var hasTrimWhitespaceBug = StringPrototype.trim && (ws.trim() || !zeroWidth.trim());
defineProperties(StringPrototype, {
    // http://blog.stevenlevithan.com/archives/faster-trim-javascript
    // http://perfectionkills.com/whitespace-deviations/
    trim: function trim() {
        if (typeof this === 'undefined' || this === null) {
            throw new TypeError("can't convert " + this + ' to object');
        }
        return String(this).replace(trimBeginRegexp, '').replace(trimEndRegexp, '');
    }
}, hasTrimWhitespaceBug);

// ES-5 15.1.2.2
if (parseInt(ws + '08') !== 8 || parseInt(ws + '0x16') !== 22) {
    /*global parseInt: true */
    parseInt = (function (origParseInt) {
        var hexRegex = /^0[xX]/;
        return function parseInt(str, radix) {
            var string = String(str).trim();
            var defaultedRadix = Number(radix) || (hexRegex.test(string) ? 16 : 10);
            return origParseInt(string, defaultedRadix);
        };
    }(parseInt));
}

}));
;
/*!
 * https://github.com/es-shims/es5-shim
 * @license es5-shim Copyright 2009-2015 by contributors, MIT License
 * see https://github.com/es-shims/es5-shim/blob/master/LICENSE
 */

// vim: ts=4 sts=4 sw=4 expandtab

//Add semicolon to prevent IIFE from being passed as argument to concated code.
;

// UMD (Universal Module Definition)
// see https://github.com/umdjs/umd/blob/master/returnExports.js
(function (root, factory) {
    'use strict';
    /*global define, exports, module */
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define(factory);
    } else if (typeof exports === 'object') {
        // Node. Does not work with strict CommonJS, but
        // only CommonJS-like enviroments that support module.exports,
        // like Node.
        module.exports = factory();
    } else {
        // Browser globals (root is window)
        root.returnExports = factory();
  }
}(this, function () {

var call = Function.prototype.call;
var prototypeOfObject = Object.prototype;
var owns = call.bind(prototypeOfObject.hasOwnProperty);

// If JS engine supports accessors creating shortcuts.
var defineGetter;
var defineSetter;
var lookupGetter;
var lookupSetter;
var supportsAccessors = owns(prototypeOfObject, '__defineGetter__');
if (supportsAccessors) {
    /*eslint-disable no-underscore-dangle */
    defineGetter = call.bind(prototypeOfObject.__defineGetter__);
    defineSetter = call.bind(prototypeOfObject.__defineSetter__);
    lookupGetter = call.bind(prototypeOfObject.__lookupGetter__);
    lookupSetter = call.bind(prototypeOfObject.__lookupSetter__);
    /*eslint-enable no-underscore-dangle */
}

// ES5 15.2.3.2
// http://es5.github.com/#x15.2.3.2
if (!Object.getPrototypeOf) {
    // https://github.com/es-shims/es5-shim/issues#issue/2
    // http://ejohn.org/blog/objectgetprototypeof/
    // recommended by fschaefer on github
    //
    // sure, and webreflection says ^_^
    // ... this will nerever possibly return null
    // ... Opera Mini breaks here with infinite loops
    Object.getPrototypeOf = function getPrototypeOf(object) {
        /*eslint-disable no-proto */
        var proto = object.__proto__;
        /*eslint-enable no-proto */
        if (proto || proto === null) {
            return proto;
        } else if (object.constructor) {
            return object.constructor.prototype;
        } else {
            return prototypeOfObject;
        }
    };
}

//ES5 15.2.3.3
//http://es5.github.com/#x15.2.3.3

function doesGetOwnPropertyDescriptorWork(object) {
    try {
        object.sentinel = 0;
        return Object.getOwnPropertyDescriptor(object, 'sentinel').value === 0;
    } catch (exception) {
        return false;
    }
}

//check whether getOwnPropertyDescriptor works if it's given. Otherwise,
//shim partially.
if (Object.defineProperty) {
    var getOwnPropertyDescriptorWorksOnObject = doesGetOwnPropertyDescriptorWork({});
    var getOwnPropertyDescriptorWorksOnDom = typeof document === 'undefined' ||
    doesGetOwnPropertyDescriptorWork(document.createElement('div'));
    if (!getOwnPropertyDescriptorWorksOnDom || !getOwnPropertyDescriptorWorksOnObject) {
        var getOwnPropertyDescriptorFallback = Object.getOwnPropertyDescriptor;
    }
}

if (!Object.getOwnPropertyDescriptor || getOwnPropertyDescriptorFallback) {
    var ERR_NON_OBJECT = 'Object.getOwnPropertyDescriptor called on a non-object: ';

    /*eslint-disable no-proto */
    Object.getOwnPropertyDescriptor = function getOwnPropertyDescriptor(object, property) {
        if ((typeof object !== 'object' && typeof object !== 'function') || object === null) {
            throw new TypeError(ERR_NON_OBJECT + object);
        }

        // make a valiant attempt to use the real getOwnPropertyDescriptor
        // for I8's DOM elements.
        if (getOwnPropertyDescriptorFallback) {
            try {
                return getOwnPropertyDescriptorFallback.call(Object, object, property);
            } catch (exception) {
                // try the shim if the real one doesn't work
            }
        }

        var descriptor;

        // If object does not owns property return undefined immediately.
        if (!owns(object, property)) {
            return descriptor;
        }

        // If object has a property then it's for sure both `enumerable` and
        // `configurable`.
        descriptor = { enumerable: true, configurable: true };

        // If JS engine supports accessor properties then property may be a
        // getter or setter.
        if (supportsAccessors) {
            // Unfortunately `__lookupGetter__` will return a getter even
            // if object has own non getter property along with a same named
            // inherited getter. To avoid misbehavior we temporary remove
            // `__proto__` so that `__lookupGetter__` will return getter only
            // if it's owned by an object.
            var prototype = object.__proto__;
            var notPrototypeOfObject = object !== prototypeOfObject;
            // avoid recursion problem, breaking in Opera Mini when
            // Object.getOwnPropertyDescriptor(Object.prototype, 'toString')
            // or any other Object.prototype accessor
            if (notPrototypeOfObject) {
                object.__proto__ = prototypeOfObject;
            }

            var getter = lookupGetter(object, property);
            var setter = lookupSetter(object, property);

            if (notPrototypeOfObject) {
                // Once we have getter and setter we can put values back.
                object.__proto__ = prototype;
            }

            if (getter || setter) {
                if (getter) {
                    descriptor.get = getter;
                }
                if (setter) {
                    descriptor.set = setter;
                }
                // If it was accessor property we're done and return here
                // in order to avoid adding `value` to the descriptor.
                return descriptor;
            }
        }

        // If we got this far we know that object has an own property that is
        // not an accessor so we set it as a value and return descriptor.
        descriptor.value = object[property];
        descriptor.writable = true;
        return descriptor;
    };
    /*eslint-enable no-proto */
}

// ES5 15.2.3.4
// http://es5.github.com/#x15.2.3.4
if (!Object.getOwnPropertyNames) {
    Object.getOwnPropertyNames = function getOwnPropertyNames(object) {
        return Object.keys(object);
    };
}

// ES5 15.2.3.5
// http://es5.github.com/#x15.2.3.5
if (!Object.create) {

    // Contributed by Brandon Benvie, October, 2012
    var createEmpty;
    var supportsProto = !({ __proto__: null } instanceof Object);
                        // the following produces false positives
                        // in Opera Mini => not a reliable check
                        // Object.prototype.__proto__ === null
    /*global document */
    if (supportsProto || typeof document === 'undefined') {
        createEmpty = function () {
            return { __proto__: null };
        };
    } else {
        // In old IE __proto__ can't be used to manually set `null`, nor does
        // any other method exist to make an object that inherits from nothing,
        // aside from Object.prototype itself. Instead, create a new global
        // object and *steal* its Object.prototype and strip it bare. This is
        // used as the prototype to create nullary objects.
        createEmpty = function () {
            var iframe = document.createElement('iframe');
            var parent = document.body || document.documentElement;
            iframe.style.display = 'none';
            parent.appendChild(iframe);
            /*eslint-disable no-script-url */
            iframe.src = 'javascript:';
            /*eslint-enable no-script-url */
            var empty = iframe.contentWindow.Object.prototype;
            parent.removeChild(iframe);
            iframe = null;
            delete empty.constructor;
            delete empty.hasOwnProperty;
            delete empty.propertyIsEnumerable;
            delete empty.isPrototypeOf;
            delete empty.toLocaleString;
            delete empty.toString;
            delete empty.valueOf;
            /*eslint-disable no-proto */
            empty.__proto__ = null;
            /*eslint-enable no-proto */

            function Empty() {}
            Empty.prototype = empty;
            // short-circuit future calls
            createEmpty = function () {
                return new Empty();
            };
            return new Empty();
        };
    }

    Object.create = function create(prototype, properties) {

        var object;
        function Type() {} // An empty constructor.

        if (prototype === null) {
            object = createEmpty();
        } else {
            if (typeof prototype !== 'object' && typeof prototype !== 'function') {
                // In the native implementation `parent` can be `null`
                // OR *any* `instanceof Object`  (Object|Function|Array|RegExp|etc)
                // Use `typeof` tho, b/c in old IE, DOM elements are not `instanceof Object`
                // like they are in modern browsers. Using `Object.create` on DOM elements
                // is...err...probably inappropriate, but the native version allows for it.
                throw new TypeError('Object prototype may only be an Object or null'); // same msg as Chrome
            }
            Type.prototype = prototype;
            object = new Type();
            // IE has no built-in implementation of `Object.getPrototypeOf`
            // neither `__proto__`, but this manually setting `__proto__` will
            // guarantee that `Object.getPrototypeOf` will work as expected with
            // objects created using `Object.create`
            /*eslint-disable no-proto */
            object.__proto__ = prototype;
            /*eslint-enable no-proto */
        }

        if (properties !== void 0) {
            Object.defineProperties(object, properties);
        }

        return object;
    };
}

// ES5 15.2.3.6
// http://es5.github.com/#x15.2.3.6

// Patch for WebKit and IE8 standard mode
// Designed by hax <hax.github.com>
// related issue: https://github.com/es-shims/es5-shim/issues#issue/5
// IE8 Reference:
//     http://msdn.microsoft.com/en-us/library/dd282900.aspx
//     http://msdn.microsoft.com/en-us/library/dd229916.aspx
// WebKit Bugs:
//     https://bugs.webkit.org/show_bug.cgi?id=36423

function doesDefinePropertyWork(object) {
    try {
        Object.defineProperty(object, 'sentinel', {});
        return 'sentinel' in object;
    } catch (exception) {
        return false;
    }
}

// check whether defineProperty works if it's given. Otherwise,
// shim partially.
if (Object.defineProperty) {
    var definePropertyWorksOnObject = doesDefinePropertyWork({});
    var definePropertyWorksOnDom = typeof document === 'undefined' ||
        doesDefinePropertyWork(document.createElement('div'));
    if (!definePropertyWorksOnObject || !definePropertyWorksOnDom) {
        var definePropertyFallback = Object.defineProperty,
            definePropertiesFallback = Object.defineProperties;
    }
}

if (!Object.defineProperty || definePropertyFallback) {
    var ERR_NON_OBJECT_DESCRIPTOR = 'Property description must be an object: ';
    var ERR_NON_OBJECT_TARGET = 'Object.defineProperty called on non-object: ';
    var ERR_ACCESSORS_NOT_SUPPORTED = 'getters & setters can not be defined on this javascript engine';

    Object.defineProperty = function defineProperty(object, property, descriptor) {
        if ((typeof object !== 'object' && typeof object !== 'function') || object === null) {
            throw new TypeError(ERR_NON_OBJECT_TARGET + object);
        }
        if ((typeof descriptor !== 'object' && typeof descriptor !== 'function') || descriptor === null) {
            throw new TypeError(ERR_NON_OBJECT_DESCRIPTOR + descriptor);
        }
        // make a valiant attempt to use the real defineProperty
        // for I8's DOM elements.
        if (definePropertyFallback) {
            try {
                return definePropertyFallback.call(Object, object, property, descriptor);
            } catch (exception) {
                // try the shim if the real one doesn't work
            }
        }

        // If it's a data property.
        if ('value' in descriptor) {
            // fail silently if 'writable', 'enumerable', or 'configurable'
            // are requested but not supported
            /*
            // alternate approach:
            if ( // can't implement these features; allow false but not true
                ('writable' in descriptor && !descriptor.writable) ||
                ('enumerable' in descriptor && !descriptor.enumerable) ||
                ('configurable' in descriptor && !descriptor.configurable)
            ))
                throw new RangeError(
                    'This implementation of Object.defineProperty does not support configurable, enumerable, or writable.'
                );
            */

            if (supportsAccessors && (lookupGetter(object, property) || lookupSetter(object, property))) {
                // As accessors are supported only on engines implementing
                // `__proto__` we can safely override `__proto__` while defining
                // a property to make sure that we don't hit an inherited
                // accessor.
                /*eslint-disable no-proto */
                var prototype = object.__proto__;
                object.__proto__ = prototypeOfObject;
                // Deleting a property anyway since getter / setter may be
                // defined on object itself.
                delete object[property];
                object[property] = descriptor.value;
                // Setting original `__proto__` back now.
                object.__proto__ = prototype;
                /*eslint-enable no-proto */
            } else {
                object[property] = descriptor.value;
            }
        } else {
            if (!supportsAccessors) {
                throw new TypeError(ERR_ACCESSORS_NOT_SUPPORTED);
            }
            // If we got that far then getters and setters can be defined !!
            if ('get' in descriptor) {
                defineGetter(object, property, descriptor.get);
            }
            if ('set' in descriptor) {
                defineSetter(object, property, descriptor.set);
            }
        }
        return object;
    };
}

// ES5 15.2.3.7
// http://es5.github.com/#x15.2.3.7
if (!Object.defineProperties || definePropertiesFallback) {
    Object.defineProperties = function defineProperties(object, properties) {
        // make a valiant attempt to use the real defineProperties
        if (definePropertiesFallback) {
            try {
                return definePropertiesFallback.call(Object, object, properties);
            } catch (exception) {
                // try the shim if the real one doesn't work
            }
        }

        for (var property in properties) {
            if (owns(properties, property) && property !== '__proto__') {
                Object.defineProperty(object, property, properties[property]);
            }
        }
        return object;
    };
}

// ES5 15.2.3.8
// http://es5.github.com/#x15.2.3.8
if (!Object.seal) {
    Object.seal = function seal(object) {
        if (Object(object) !== object) {
            throw new TypeError('Object.seal can only be called on Objects.');
        }
        // this is misleading and breaks feature-detection, but
        // allows "securable" code to "gracefully" degrade to working
        // but insecure code.
        return object;
    };
}

// ES5 15.2.3.9
// http://es5.github.com/#x15.2.3.9
if (!Object.freeze) {
    Object.freeze = function freeze(object) {
        if (Object(object) !== object) {
            throw new TypeError('Object.freeze can only be called on Objects.');
        }
        // this is misleading and breaks feature-detection, but
        // allows "securable" code to "gracefully" degrade to working
        // but insecure code.
        return object;
    };
}

// detect a Rhino bug and patch it
try {
    Object.freeze(function () {});
} catch (exception) {
    Object.freeze = (function freeze(freezeObject) {
        return function freeze(object) {
            if (typeof object === 'function') {
                return object;
            } else {
                return freezeObject(object);
            }
        };
    }(Object.freeze));
}

// ES5 15.2.3.10
// http://es5.github.com/#x15.2.3.10
if (!Object.preventExtensions) {
    Object.preventExtensions = function preventExtensions(object) {
        if (Object(object) !== object) {
            throw new TypeError('Object.preventExtensions can only be called on Objects.');
        }
        // this is misleading and breaks feature-detection, but
        // allows "securable" code to "gracefully" degrade to working
        // but insecure code.
        return object;
    };
}

// ES5 15.2.3.11
// http://es5.github.com/#x15.2.3.11
if (!Object.isSealed) {
    Object.isSealed = function isSealed(object) {
        if (Object(object) !== object) {
            throw new TypeError('Object.isSealed can only be called on Objects.');
        }
        return false;
    };
}

// ES5 15.2.3.12
// http://es5.github.com/#x15.2.3.12
if (!Object.isFrozen) {
    Object.isFrozen = function isFrozen(object) {
        if (Object(object) !== object) {
            throw new TypeError('Object.isFrozen can only be called on Objects.');
        }
        return false;
    };
}

// ES5 15.2.3.13
// http://es5.github.com/#x15.2.3.13
if (!Object.isExtensible) {
    Object.isExtensible = function isExtensible(object) {
        // 1. If Type(O) is not Object throw a TypeError exception.
        if (Object(object) !== object) {
            throw new TypeError('Object.isExtensible can only be called on Objects.');
        }
        // 2. Return the Boolean value of the [[Extensible]] internal property of O.
        var name = '';
        while (owns(object, name)) {
            name += '?';
        }
        object[name] = true;
        var returnValue = owns(object, name);
        delete object[name];
        return returnValue;
    };
}

}));
;
 /*!
  * https://github.com/paulmillr/es6-shim
  * @license es6-shim Copyright 2013-2015 by Paul Miller (http://paulmillr.com)
  *   and contributors,  MIT License
  * es6-shim: v0.27.1
  * see https://github.com/paulmillr/es6-shim/blob/0.27.1/LICENSE
  * Details and documentation:
  * https://github.com/paulmillr/es6-shim/
  */

// UMD (Universal Module Definition)
// see https://github.com/umdjs/umd/blob/master/returnExports.js
(function (root, factory) {
  /*global define, module, exports */
  if (typeof define === 'function' && define.amd) {
    // AMD. Register as an anonymous module.
    define(factory);
  } else if (typeof exports === 'object') {
    // Node. Does not work with strict CommonJS, but
    // only CommonJS-like enviroments that support module.exports,
    // like Node.
    module.exports = factory();
  } else {
    // Browser globals (root is window)
    root.returnExports = factory();
  }
}(this, function () {
  'use strict';

  var not = function notThunker(func) {
    return function notThunk() { return !func.apply(this, arguments); };
  };
  var throwsError = function (func) {
    try {
      func();
      return false;
    } catch (e) {
      return true;
    }
  };
  var valueOrFalseIfThrows = function valueOrFalseIfThrows(func) {
    try {
      return func();
    } catch (e) {
      return false;
    }
  };

  var isCallableWithoutNew = not(throwsError);
  var arePropertyDescriptorsSupported = function () {
    // if Object.defineProperty exists but throws, it's IE 8
    return !throwsError(function () { Object.defineProperty({}, 'x', {}); });
  };
  var supportsDescriptors = !!Object.defineProperty && arePropertyDescriptorsSupported();

  var defineProperty = function (object, name, value, force) {
    if (!force && name in object) { return; }
    if (supportsDescriptors) {
      Object.defineProperty(object, name, {
        configurable: true,
        enumerable: false,
        writable: true,
        value: value
      });
    } else {
      object[name] = value;
    }
  };

  // Define configurable, writable and non-enumerable props
  // if they don’t exist.
  var defineProperties = function (object, map) {
    Object.keys(map).forEach(function (name) {
      var method = map[name];
      defineProperty(object, name, method, false);
    });
  };

  // Simple shim for Object.create on ES3 browsers
  // (unlike real shim, no attempt to support `prototype === null`)
  var create = Object.create || function (prototype, properties) {
    function Prototype() {}
    Prototype.prototype = prototype;
    var object = new Prototype();
    if (typeof properties !== 'undefined') {
      defineProperties(object, properties);
    }
    return object;
  };

  var supportsSubclassing = function (C, f) {
    if (!Object.setPrototypeOf) { return false; /* skip test on IE < 11 */ }
    return valueOrFalseIfThrows(function () {
      var Sub = function Subclass(arg) {
        var o = new C(arg);
        Object.setPrototypeOf(o, Subclass.prototype);
        return o;
      };
      Sub.prototype = create(C.prototype, {
        constructor: { value: C }
      });
      return f(Sub);
    });
  };

  var startsWithRejectsRegex = function () {
    return String.prototype.startsWith && throwsError(function () {
      /* throws if spec-compliant */
      '/a/'.startsWith(/a/);
    });
  };
  var startsWithHandlesInfinity = (function () {
    return String.prototype.startsWith && 'abc'.startsWith('a', Infinity) === false;
  }());

  /*jshint evil: true */
  var getGlobal = new Function('return this;');
  /*jshint evil: false */

  var globals = getGlobal();
  var globalIsFinite = globals.isFinite;
  var hasStrictMode = (function () { return this === null; }.call(null));
  var startsWithIsCompliant = startsWithRejectsRegex() && startsWithHandlesInfinity;
  var _indexOf = Function.call.bind(String.prototype.indexOf);
  var _toString = Function.call.bind(Object.prototype.toString);
  var _hasOwnProperty = Function.call.bind(Object.prototype.hasOwnProperty);
  var ArrayIterator; // make our implementation private
  var noop = function () {};

  var Symbol = globals.Symbol || {};
  var symbolSpecies = Symbol.species || '@@species';
  var Type = {
    object: function (x) { return x !== null && typeof x === 'object'; },
    string: function (x) { return _toString(x) === '[object String]'; },
    regex: function (x) { return _toString(x) === '[object RegExp]'; },
    symbol: function (x) {
      return typeof globals.Symbol === 'function' && typeof x === 'symbol';
    }
  };

  var numberIsNaN = Number.isNaN || function isNaN(value) {
    // NaN !== NaN, but they are identical.
    // NaNs are the only non-reflexive value, i.e., if x !== x,
    // then x is NaN.
    // isNaN is broken: it converts its argument to number, so
    // isNaN('foo') => true
    return value !== value;
  };
  var numberIsFinite = Number.isFinite || function isFinite(value) {
    return typeof value === 'number' && globalIsFinite(value);
  };

  var Value = {
    getter: function (object, name, getter) {
      if (!supportsDescriptors) {
        throw new TypeError('getters require true ES5 support');
      }
      Object.defineProperty(object, name, {
        configurable: true,
        enumerable: false,
        get: getter
      });
    },
    proxy: function (originalObject, key, targetObject) {
      if (!supportsDescriptors) {
        throw new TypeError('getters require true ES5 support');
      }
      var originalDescriptor = Object.getOwnPropertyDescriptor(originalObject, key);
      Object.defineProperty(targetObject, key, {
        configurable: originalDescriptor.configurable,
        enumerable: originalDescriptor.enumerable,
        get: function getKey() { return originalObject[key]; },
        set: function setKey(value) { originalObject[key] = value; }
      });
    },
    redefine: function (object, property, newValue) {
      if (supportsDescriptors) {
        var descriptor = Object.getOwnPropertyDescriptor(object, property);
        descriptor.value = newValue;
        Object.defineProperty(object, property, descriptor);
      } else {
        object[property] = newValue;
      }
    },
    preserveToString: function (target, source) {
      defineProperty(target, 'toString', source.toString.bind(source), true);
    }
  };

  var overrideNative = function overrideNative(object, property, replacement) {
    var original = object[property];
    defineProperty(object, property, replacement, true);
    Value.preserveToString(object[property], original);
  };

  // This is a private name in the es6 spec, equal to '[Symbol.iterator]'
  // we're going to use an arbitrary _-prefixed name to make our shims
  // work properly with each other, even though we don't have full Iterator
  // support.  That is, `Array.from(map.keys())` will work, but we don't
  // pretend to export a "real" Iterator interface.
  var $iterator$ = Type.symbol(Symbol.iterator) ? Symbol.iterator : '_es6-shim iterator_';
  // Firefox ships a partial implementation using the name @@iterator.
  // https://bugzilla.mozilla.org/show_bug.cgi?id=907077#c14
  // So use that name if we detect it.
  if (globals.Set && typeof new globals.Set()['@@iterator'] === 'function') {
    $iterator$ = '@@iterator';
  }
  var addIterator = function (prototype, impl) {
    var implementation = impl || function iterator() { return this; };
    var o = {};
    o[$iterator$] = implementation;
    defineProperties(prototype, o);
    if (!prototype[$iterator$] && Type.symbol($iterator$)) {
      // implementations are buggy when $iterator$ is a Symbol
      prototype[$iterator$] = implementation;
    }
  };

  // taken directly from https://github.com/ljharb/is-arguments/blob/master/index.js
  // can be replaced with require('is-arguments') if we ever use a build process instead
  var isArguments = function isArguments(value) {
    var str = _toString(value);
    var result = str === '[object Arguments]';
    if (!result) {
      result = str !== '[object Array]' &&
        value !== null &&
        typeof value === 'object' &&
        typeof value.length === 'number' &&
        value.length >= 0 &&
        _toString(value.callee) === '[object Function]';
    }
    return result;
  };

  var safeApply = Function.call.bind(Function.apply);

  var ES = {
    // https://people.mozilla.org/~jorendorff/es6-draft.html#sec-call-f-v-args
    Call: function Call(F, V) {
      var args = arguments.length > 2 ? arguments[2] : [];
      if (!ES.IsCallable(F)) {
        throw new TypeError(F + ' is not a function');
      }
      return safeApply(F, V, args);
    },

    RequireObjectCoercible: function (x, optMessage) {
      /* jshint eqnull:true */
      if (x == null) {
        throw new TypeError(optMessage || 'Cannot call method on ' + x);
      }
    },

    TypeIsObject: function (x) {
      /* jshint eqnull:true */
      // this is expensive when it returns false; use this function
      // when you expect it to return true in the common case.
      return x != null && Object(x) === x;
    },

    ToObject: function (o, optMessage) {
      ES.RequireObjectCoercible(o, optMessage);
      return Object(o);
    },

    IsCallable: function (x) {
      // some versions of IE say that typeof /abc/ === 'function'
      return typeof x === 'function' && _toString(x) === '[object Function]';
    },

    ToInt32: function (x) {
      return ES.ToNumber(x) >> 0;
    },

    ToUint32: function (x) {
      return ES.ToNumber(x) >>> 0;
    },

    ToNumber: function (value) {
      if (_toString(value) === '[object Symbol]') {
        throw new TypeError('Cannot convert a Symbol value to a number');
      }
      return +value;
    },

    ToInteger: function (value) {
      var number = ES.ToNumber(value);
      if (numberIsNaN(number)) { return 0; }
      if (number === 0 || !numberIsFinite(number)) { return number; }
      return (number > 0 ? 1 : -1) * Math.floor(Math.abs(number));
    },

    ToLength: function (value) {
      var len = ES.ToInteger(value);
      if (len <= 0) { return 0; } // includes converting -0 to +0
      if (len > Number.MAX_SAFE_INTEGER) { return Number.MAX_SAFE_INTEGER; }
      return len;
    },

    SameValue: function (a, b) {
      if (a === b) {
        // 0 === -0, but they are not identical.
        if (a === 0) { return 1 / a === 1 / b; }
        return true;
      }
      return numberIsNaN(a) && numberIsNaN(b);
    },

    SameValueZero: function (a, b) {
      // same as SameValue except for SameValueZero(+0, -0) == true
      return (a === b) || (numberIsNaN(a) && numberIsNaN(b));
    },

    IsIterable: function (o) {
      return ES.TypeIsObject(o) && (typeof o[$iterator$] !== 'undefined' || isArguments(o));
    },

    GetIterator: function (o) {
      if (isArguments(o)) {
        // special case support for `arguments`
        return new ArrayIterator(o, 'value');
      }
      var itFn = o[$iterator$];
      if (!ES.IsCallable(itFn)) {
        throw new TypeError('value is not an iterable');
      }
      var it = itFn.call(o);
      if (!ES.TypeIsObject(it)) {
        throw new TypeError('bad iterator');
      }
      return it;
    },

    IteratorNext: function (it) {
      var result = arguments.length > 1 ? it.next(arguments[1]) : it.next();
      if (!ES.TypeIsObject(result)) {
        throw new TypeError('bad iterator');
      }
      return result;
    },

    Construct: function (C, args) {
      // CreateFromConstructor
      var obj;
      if (ES.IsCallable(C[symbolSpecies])) {
        obj = C[symbolSpecies]();
      } else {
        // OrdinaryCreateFromConstructor
        obj = create(C.prototype || null);
      }
      // Mark that we've used the es6 construct path
      // (see emulateES6construct)
      defineProperties(obj, { _es6construct: true });
      // Call the constructor.
      var result = ES.Call(C, obj, args);
      return ES.TypeIsObject(result) ? result : obj;
    },

    CreateHTML: function (string, tag, attribute, value) {
      var S = String(string);
      var p1 = '<' + tag;
      if (attribute !== '') {
        var V = String(value);
        var escapedV = V.replace(/"/g, '&quot;');
        p1 += ' ' + attribute + '="' + escapedV + '"';
      }
      var p2 = p1 + '>';
      var p3 = p2 + S;
      return p3 + '</' + tag + '>';
    }
  };

  var emulateES6construct = function (o) {
    if (!ES.TypeIsObject(o)) { throw new TypeError('bad object'); }
    var object = o;
    // es5 approximation to es6 subclass semantics: in es6, 'new Foo'
    // would invoke Foo.@@species to allocation/initialize the new object.
    // In es5 we just get the plain object.  So if we detect an
    // uninitialized object, invoke o.constructor.@@species
    if (!object._es6construct) {
      if (object.constructor && ES.IsCallable(object.constructor[symbolSpecies])) {
        object = object.constructor[symbolSpecies](object);
      }
      defineProperties(object, { _es6construct: true });
    }
    return object;
  };

  // Firefox 31 reports this function's length as 0
  // https://bugzilla.mozilla.org/show_bug.cgi?id=1062484
  if (String.fromCodePoint && String.fromCodePoint.length !== 1) {
    var originalFromCodePoint = Function.apply.bind(String.fromCodePoint);
    overrideNative(String, 'fromCodePoint', function fromCodePoint(codePoints) { return originalFromCodePoint(this, arguments); });
  }

  var StringShims = {
    fromCodePoint: function fromCodePoint(codePoints) {
      var result = [];
      var next;
      for (var i = 0, length = arguments.length; i < length; i++) {
        next = Number(arguments[i]);
        if (!ES.SameValue(next, ES.ToInteger(next)) || next < 0 || next > 0x10FFFF) {
          throw new RangeError('Invalid code point ' + next);
        }

        if (next < 0x10000) {
          result.push(String.fromCharCode(next));
        } else {
          next -= 0x10000;
          result.push(String.fromCharCode((next >> 10) + 0xD800));
          result.push(String.fromCharCode((next % 0x400) + 0xDC00));
        }
      }
      return result.join('');
    },

    raw: function raw(callSite) {
      var cooked = ES.ToObject(callSite, 'bad callSite');
      var rawString = ES.ToObject(cooked.raw, 'bad raw value');
      var len = rawString.length;
      var literalsegments = ES.ToLength(len);
      if (literalsegments <= 0) {
        return '';
      }

      var stringElements = [];
      var nextIndex = 0;
      var nextKey, next, nextSeg, nextSub;
      while (nextIndex < literalsegments) {
        nextKey = String(nextIndex);
        nextSeg = String(rawString[nextKey]);
        stringElements.push(nextSeg);
        if (nextIndex + 1 >= literalsegments) {
          break;
        }
        next = nextIndex + 1 < arguments.length ? arguments[nextIndex + 1] : '';
        nextSub = String(next);
        stringElements.push(nextSub);
        nextIndex++;
      }
      return stringElements.join('');
    }
  };
  defineProperties(String, StringShims);
  if (String.raw({ raw: { 0: 'x', 1: 'y', length: 2 } }) !== 'xy') {
    // IE 11 TP has a broken String.raw implementation
    overrideNative(String, 'raw', StringShims.raw);
  }

  // Fast repeat, uses the `Exponentiation by squaring` algorithm.
  // Perf: http://jsperf.com/string-repeat2/2
  var stringRepeat = function repeat(s, times) {
    if (times < 1) { return ''; }
    if (times % 2) { return repeat(s, times - 1) + s; }
    var half = repeat(s, times / 2);
    return half + half;
  };
  var stringMaxLength = Infinity;

  var StringPrototypeShims = {
    repeat: function repeat(times) {
      ES.RequireObjectCoercible(this);
      var thisStr = String(this);
      var numTimes = ES.ToInteger(times);
      if (numTimes < 0 || numTimes >= stringMaxLength) {
        throw new RangeError('repeat count must be less than infinity and not overflow maximum string size');
      }
      return stringRepeat(thisStr, numTimes);
    },

    startsWith: function startsWith(searchString) {
      ES.RequireObjectCoercible(this);
      var thisStr = String(this);
      if (Type.regex(searchString)) {
        throw new TypeError('Cannot call method "startsWith" with a regex');
      }
      var searchStr = String(searchString);
      var startArg = arguments.length > 1 ? arguments[1] : void 0;
      var start = Math.max(ES.ToInteger(startArg), 0);
      return thisStr.slice(start, start + searchStr.length) === searchStr;
    },

    endsWith: function endsWith(searchString) {
      ES.RequireObjectCoercible(this);
      var thisStr = String(this);
      if (Type.regex(searchString)) {
        throw new TypeError('Cannot call method "endsWith" with a regex');
      }
      var searchStr = String(searchString);
      var thisLen = thisStr.length;
      var posArg = arguments.length > 1 ? arguments[1] : void 0;
      var pos = typeof posArg === 'undefined' ? thisLen : ES.ToInteger(posArg);
      var end = Math.min(Math.max(pos, 0), thisLen);
      return thisStr.slice(end - searchStr.length, end) === searchStr;
    },

    includes: function includes(searchString) {
      var position = arguments.length > 1 ? arguments[1] : void 0;
      // Somehow this trick makes method 100% compat with the spec.
      return _indexOf(this, searchString, position) !== -1;
    },

    codePointAt: function codePointAt(pos) {
      ES.RequireObjectCoercible(this);
      var thisStr = String(this);
      var position = ES.ToInteger(pos);
      var length = thisStr.length;
      if (position >= 0 && position < length) {
        var first = thisStr.charCodeAt(position);
        var isEnd = (position + 1 === length);
        if (first < 0xD800 || first > 0xDBFF || isEnd) { return first; }
        var second = thisStr.charCodeAt(position + 1);
        if (second < 0xDC00 || second > 0xDFFF) { return first; }
        return ((first - 0xD800) * 1024) + (second - 0xDC00) + 0x10000;
      }
    }
  };
  defineProperties(String.prototype, StringPrototypeShims);

  if ('a'.includes('a', Infinity) !== false) {
    overrideNative(String.prototype, 'includes', StringPrototypeShims.includes);
  }

  var hasStringTrimBug = '\u0085'.trim().length !== 1;
  if (hasStringTrimBug) {
    delete String.prototype.trim;
    // whitespace from: http://es5.github.io/#x15.5.4.20
    // implementation from https://github.com/es-shims/es5-shim/blob/v3.4.0/es5-shim.js#L1304-L1324
    var ws = [
      '\x09\x0A\x0B\x0C\x0D\x20\xA0\u1680\u180E\u2000\u2001\u2002\u2003',
      '\u2004\u2005\u2006\u2007\u2008\u2009\u200A\u202F\u205F\u3000\u2028',
      '\u2029\uFEFF'
    ].join('');
    var trimRegexp = new RegExp('(^[' + ws + ']+)|([' + ws + ']+$)', 'g');
    defineProperties(String.prototype, {
      trim: function trim() {
        if (typeof this === 'undefined' || this === null) {
          throw new TypeError("can't convert " + this + ' to object');
        }
        return String(this).replace(trimRegexp, '');
      }
    });
  }

  // see https://people.mozilla.org/~jorendorff/es6-draft.html#sec-string.prototype-@@iterator
  var StringIterator = function (s) {
    ES.RequireObjectCoercible(s);
    this._s = String(s);
    this._i = 0;
  };
  StringIterator.prototype.next = function () {
    var s = this._s, i = this._i;
    if (typeof s === 'undefined' || i >= s.length) {
      this._s = void 0;
      return { value: void 0, done: true };
    }
    var first = s.charCodeAt(i), second, len;
    if (first < 0xD800 || first > 0xDBFF || (i + 1) === s.length) {
      len = 1;
    } else {
      second = s.charCodeAt(i + 1);
      len = (second < 0xDC00 || second > 0xDFFF) ? 1 : 2;
    }
    this._i = i + len;
    return { value: s.substr(i, len), done: false };
  };
  addIterator(StringIterator.prototype);
  addIterator(String.prototype, function () {
    return new StringIterator(this);
  });

  if (!startsWithIsCompliant) {
    // Firefox (< 37?) and IE 11 TP have a noncompliant startsWith implementation
    overrideNative(String.prototype, 'startsWith', StringPrototypeShims.startsWith);
    overrideNative(String.prototype, 'endsWith', StringPrototypeShims.endsWith);
  }

  var ArrayShims = {
    from: function from(iterable) {
      var mapFn = arguments.length > 1 ? arguments[1] : void 0;

      var list = ES.ToObject(iterable, 'bad iterable');
      if (typeof mapFn !== 'undefined' && !ES.IsCallable(mapFn)) {
        throw new TypeError('Array.from: when provided, the second argument must be a function');
      }

      var hasThisArg = arguments.length > 2;
      var thisArg = hasThisArg ? arguments[2] : void 0;

      var usingIterator = ES.IsIterable(list);
      // does the spec really mean that Arrays should use ArrayIterator?
      // https://bugs.ecmascript.org/show_bug.cgi?id=2416
      //if (Array.isArray(list)) { usingIterator=false; }

      var length;
      var result, i, value;
      if (usingIterator) {
        i = 0;
        result = ES.IsCallable(this) ? Object(new this()) : [];
        var it = usingIterator ? ES.GetIterator(list) : null;
        var iterationValue;

        do {
          iterationValue = ES.IteratorNext(it);
          if (!iterationValue.done) {
            value = iterationValue.value;
            if (mapFn) {
              result[i] = hasThisArg ? mapFn.call(thisArg, value, i) : mapFn(value, i);
            } else {
              result[i] = value;
            }
            i += 1;
          }
        } while (!iterationValue.done);
        length = i;
      } else {
        length = ES.ToLength(list.length);
        result = ES.IsCallable(this) ? Object(new this(length)) : new Array(length);
        for (i = 0; i < length; ++i) {
          value = list[i];
          if (mapFn) {
            result[i] = hasThisArg ? mapFn.call(thisArg, value, i) : mapFn(value, i);
          } else {
            result[i] = value;
          }
        }
      }

      result.length = length;
      return result;
    },

    of: function of() {
      return Array.from.call(this, arguments);
    }
  };
  defineProperties(Array, ArrayShims);

  // Given an argument x, it will return an IteratorResult object,
  // with value set to x and done to false.
  // Given no arguments, it will return an iterator completion object.
  var iteratorResult = function (x) {
    return { value: x, done: arguments.length === 0 };
  };

  // Our ArrayIterator is private; see
  // https://github.com/paulmillr/es6-shim/issues/252
  ArrayIterator = function (array, kind) {
      this.i = 0;
      this.array = array;
      this.kind = kind;
  };

  defineProperties(ArrayIterator.prototype, {
    next: function () {
      var i = this.i, array = this.array;
      if (!(this instanceof ArrayIterator)) {
        throw new TypeError('Not an ArrayIterator');
      }
      if (typeof array !== 'undefined') {
        var len = ES.ToLength(array.length);
        for (; i < len; i++) {
          var kind = this.kind;
          var retval;
          if (kind === 'key') {
            retval = i;
          } else if (kind === 'value') {
            retval = array[i];
          } else if (kind === 'entry') {
            retval = [i, array[i]];
          }
          this.i = i + 1;
          return { value: retval, done: false };
        }
      }
      this.array = void 0;
      return { value: void 0, done: true };
    }
  });
  addIterator(ArrayIterator.prototype);

  var ObjectIterator = function (object, kind) {
    this.object = object;
    // Don't generate keys yet.
    this.array = null;
    this.kind = kind;
  };

  function getAllKeys(object) {
    var keys = [];

    for (var key in object) {
      keys.push(key);
    }

    return keys;
  }

  defineProperties(ObjectIterator.prototype, {
    next: function () {
      var key, array = this.array;

      if (!(this instanceof ObjectIterator)) {
        throw new TypeError('Not an ObjectIterator');
      }

      // Keys not generated
      if (array === null) {
        array = this.array = getAllKeys(this.object);
      }

      // Find next key in the object
      while (ES.ToLength(array.length) > 0) {
        key = array.shift();

        // The candidate key isn't defined on object.
        // Must have been deleted, or object[[Prototype]]
        // has been modified.
        if (!(key in this.object)) {
          continue;
        }

        if (this.kind === 'key') {
          return iteratorResult(key);
        } else if (this.kind === 'value') {
          return iteratorResult(this.object[key]);
        } else {
          return iteratorResult([key, this.object[key]]);
        }
      }

      return iteratorResult();
    }
  });
  addIterator(ObjectIterator.prototype);

  // note: this is positioned here because it depends on ArrayIterator
  var arrayOfSupportsSubclassing = (function () {
    // Detects a bug in Webkit nightly r181886
    var Foo = function Foo(len) { this.length = len; };
    Foo.prototype = [];
    var fooArr = Array.of.apply(Foo, [1, 2]);
    return fooArr instanceof Foo && fooArr.length === 2;
  }());
  if (!arrayOfSupportsSubclassing) {
    overrideNative(Array, 'of', ArrayShims.of);
  }

  var ArrayPrototypeShims = {
    copyWithin: function copyWithin(target, start) {
      var end = arguments[2]; // copyWithin.length must be 2
      var o = ES.ToObject(this);
      var len = ES.ToLength(o.length);
      var relativeTarget = ES.ToInteger(target);
      var relativeStart = ES.ToInteger(start);
      var to = relativeTarget < 0 ? Math.max(len + relativeTarget, 0) : Math.min(relativeTarget, len);
      var from = relativeStart < 0 ? Math.max(len + relativeStart, 0) : Math.min(relativeStart, len);
      end = typeof end === 'undefined' ? len : ES.ToInteger(end);
      var fin = end < 0 ? Math.max(len + end, 0) : Math.min(end, len);
      var count = Math.min(fin - from, len - to);
      var direction = 1;
      if (from < to && to < (from + count)) {
        direction = -1;
        from += count - 1;
        to += count - 1;
      }
      while (count > 0) {
        if (_hasOwnProperty(o, from)) {
          o[to] = o[from];
        } else {
          delete o[from];
        }
        from += direction;
        to += direction;
        count -= 1;
      }
      return o;
    },

    fill: function fill(value) {
      var start = arguments.length > 1 ? arguments[1] : void 0;
      var end = arguments.length > 2 ? arguments[2] : void 0;
      var O = ES.ToObject(this);
      var len = ES.ToLength(O.length);
      start = ES.ToInteger(typeof start === 'undefined' ? 0 : start);
      end = ES.ToInteger(typeof end === 'undefined' ? len : end);

      var relativeStart = start < 0 ? Math.max(len + start, 0) : Math.min(start, len);
      var relativeEnd = end < 0 ? len + end : end;

      for (var i = relativeStart; i < len && i < relativeEnd; ++i) {
        O[i] = value;
      }
      return O;
    },

    find: function find(predicate) {
      var list = ES.ToObject(this);
      var length = ES.ToLength(list.length);
      if (!ES.IsCallable(predicate)) {
        throw new TypeError('Array#find: predicate must be a function');
      }
      var thisArg = arguments.length > 1 ? arguments[1] : null;
      for (var i = 0, value; i < length; i++) {
        value = list[i];
        if (thisArg) {
          if (predicate.call(thisArg, value, i, list)) { return value; }
        } else if (predicate(value, i, list)) {
          return value;
        }
      }
    },

    findIndex: function findIndex(predicate) {
      var list = ES.ToObject(this);
      var length = ES.ToLength(list.length);
      if (!ES.IsCallable(predicate)) {
        throw new TypeError('Array#findIndex: predicate must be a function');
      }
      var thisArg = arguments.length > 1 ? arguments[1] : null;
      for (var i = 0; i < length; i++) {
        if (thisArg) {
          if (predicate.call(thisArg, list[i], i, list)) { return i; }
        } else if (predicate(list[i], i, list)) {
          return i;
        }
      }
      return -1;
    },

    keys: function keys() {
      return new ArrayIterator(this, 'key');
    },

    values: function values() {
      return new ArrayIterator(this, 'value');
    },

    entries: function entries() {
      return new ArrayIterator(this, 'entry');
    }
  };
  // Safari 7.1 defines Array#keys and Array#entries natively,
  // but the resulting ArrayIterator objects don't have a "next" method.
  if (Array.prototype.keys && !ES.IsCallable([1].keys().next)) {
    delete Array.prototype.keys;
  }
  if (Array.prototype.entries && !ES.IsCallable([1].entries().next)) {
    delete Array.prototype.entries;
  }

  // Chrome 38 defines Array#keys and Array#entries, and Array#@@iterator, but not Array#values
  if (Array.prototype.keys && Array.prototype.entries && !Array.prototype.values && Array.prototype[$iterator$]) {
    defineProperties(Array.prototype, {
      values: Array.prototype[$iterator$]
    });
    if (Type.symbol(Symbol.unscopables)) {
      Array.prototype[Symbol.unscopables].values = true;
    }
  }
  // Chrome 40 defines Array#values with the incorrect name, although Array#{keys,entries} have the correct name
  if (Array.prototype.values && Array.prototype.values.name !== 'values') {
    var originalArrayPrototypeValues = Array.prototype.values;
    overrideNative(Array.prototype, 'values', function values() { return originalArrayPrototypeValues.call(this); });
    defineProperty(Array.prototype, $iterator$, Array.prototype.values, true);
  }
  defineProperties(Array.prototype, ArrayPrototypeShims);

  addIterator(Array.prototype, function () { return this.values(); });
  // Chrome defines keys/values/entries on Array, but doesn't give us
  // any way to identify its iterator.  So add our own shimmed field.
  if (Object.getPrototypeOf) {
    addIterator(Object.getPrototypeOf([].values()));
  }

  // note: this is positioned here because it relies on Array#entries
  var arrayFromSwallowsNegativeLengths = (function () {
    // Detects a Firefox bug in v32
    // https://bugzilla.mozilla.org/show_bug.cgi?id=1063993
    return valueOrFalseIfThrows(function () { return Array.from({ length: -1 }).length === 0; });
  }());
  var arrayFromHandlesIterables = (function () {
    // Detects a bug in Webkit nightly r181886
    var arr = Array.from([0].entries());
    return arr.length === 1 && arr[0][0] === 0 && arr[0][1] === 1;
  }());
  if (!arrayFromSwallowsNegativeLengths || !arrayFromHandlesIterables) {
    overrideNative(Array, 'from', ArrayShims.from);
  }

  var maxSafeInteger = Math.pow(2, 53) - 1;
  defineProperties(Number, {
    MAX_SAFE_INTEGER: maxSafeInteger,
    MIN_SAFE_INTEGER: -maxSafeInteger,
    EPSILON: 2.220446049250313e-16,

    parseInt: globals.parseInt,
    parseFloat: globals.parseFloat,

    isFinite: numberIsFinite,

    isInteger: function isInteger(value) {
      return numberIsFinite(value) && ES.ToInteger(value) === value;
    },

    isSafeInteger: function isSafeInteger(value) {
      return Number.isInteger(value) && Math.abs(value) <= Number.MAX_SAFE_INTEGER;
    },

    isNaN: numberIsNaN
  });
  // Firefox 37 has a conforming Number.parseInt, but it's not === to the global parseInt (fixed in v40)
  defineProperty(Number, 'parseInt', globals.parseInt, Number.parseInt !== globals.parseInt);

  // Work around bugs in Array#find and Array#findIndex -- early
  // implementations skipped holes in sparse arrays. (Note that the
  // implementations of find/findIndex indirectly use shimmed
  // methods of Number, so this test has to happen down here.)
  /*jshint elision: true */
  if (![, 1].find(function (item, idx) { return idx === 0; })) {
    overrideNative(Array.prototype, 'find', ArrayPrototypeShims.find);
  }
  if ([, 1].findIndex(function (item, idx) { return idx === 0; }) !== 0) {
    overrideNative(Array.prototype, 'findIndex', ArrayPrototypeShims.findIndex);
  }
  /*jshint elision: false */

  var isEnumerableOn = Function.bind.call(Function.bind, Object.prototype.propertyIsEnumerable);
  var sliceArgs = function sliceArgs() {
    // per https://github.com/petkaantonov/bluebird/wiki/Optimization-killers#32-leaking-arguments
    // and https://gist.github.com/WebReflection/4327762cb87a8c634a29
    var initial = Number(this);
    var len = arguments.length;
    var desiredArgCount = len - initial;
    var args = new Array(desiredArgCount < 0 ? 0 : desiredArgCount);
    for (var i = initial; i < len; ++i) {
      args[i - initial] = arguments[i];
    }
    return args;
  };
  var assignTo = function assignTo(source) {
    return function assignToSource(target, key) {
      target[key] = source[key];
      return target;
    };
  };
  var assignReducer = function (target, source) {
    var keys = Object.keys(Object(source));
    var symbols;
    if (ES.IsCallable(Object.getOwnPropertySymbols)) {
      symbols = Object.getOwnPropertySymbols(Object(source)).filter(isEnumerableOn(source));
    }
    return keys.concat(symbols || []).reduce(assignTo(source), target);
  };

  var ObjectShims = {
    // 19.1.3.1
    assign: function (target, source) {
      if (!ES.TypeIsObject(target)) {
        throw new TypeError('target must be an object');
      }
      return Array.prototype.reduce.call(sliceArgs.apply(0, arguments), assignReducer);
    },

    // Added in WebKit in https://bugs.webkit.org/show_bug.cgi?id=143865
    is: function is(a, b) {
      return ES.SameValue(a, b);
    }
  };
  var assignHasPendingExceptions = Object.assign && Object.preventExtensions && (function () {
    // Firefox 37 still has "pending exception" logic in its Object.assign implementation,
    // which is 72% slower than our shim, and Firefox 40's native implementation.
    var thrower = Object.preventExtensions({ 1: 2 });
    try {
      Object.assign(thrower, 'xy');
    } catch (e) {
      return thrower[1] === 'y';
    }
  }());
  if (assignHasPendingExceptions) {
    overrideNative(Object, 'assign', ObjectShims.assign);
  }
  defineProperties(Object, ObjectShims);

  if (supportsDescriptors) {
    var ES5ObjectShims = {
      // 19.1.3.9
      // shim from https://gist.github.com/WebReflection/5593554
      setPrototypeOf: (function (Object, magic) {
        var set;

        var checkArgs = function (O, proto) {
          if (!ES.TypeIsObject(O)) {
            throw new TypeError('cannot set prototype on a non-object');
          }
          if (!(proto === null || ES.TypeIsObject(proto))) {
            throw new TypeError('can only set prototype to an object or null' + proto);
          }
        };

        var setPrototypeOf = function (O, proto) {
          checkArgs(O, proto);
          set.call(O, proto);
          return O;
        };

        try {
          // this works already in Firefox and Safari
          set = Object.getOwnPropertyDescriptor(Object.prototype, magic).set;
          set.call({}, null);
        } catch (e) {
          if (Object.prototype !== {}[magic]) {
            // IE < 11 cannot be shimmed
            return;
          }
          // probably Chrome or some old Mobile stock browser
          set = function (proto) {
            this[magic] = proto;
          };
          // please note that this will **not** work
          // in those browsers that do not inherit
          // __proto__ by mistake from Object.prototype
          // in these cases we should probably throw an error
          // or at least be informed about the issue
          setPrototypeOf.polyfill = setPrototypeOf(
            setPrototypeOf({}, null),
            Object.prototype
          ) instanceof Object;
          // setPrototypeOf.polyfill === true means it works as meant
          // setPrototypeOf.polyfill === false means it's not 100% reliable
          // setPrototypeOf.polyfill === undefined
          // or
          // setPrototypeOf.polyfill ==  null means it's not a polyfill
          // which means it works as expected
          // we can even delete Object.prototype.__proto__;
        }
        return setPrototypeOf;
      }(Object, '__proto__'))
    };

    defineProperties(Object, ES5ObjectShims);
  }

  // Workaround bug in Opera 12 where setPrototypeOf(x, null) doesn't work,
  // but Object.create(null) does.
  if (Object.setPrototypeOf && Object.getPrototypeOf &&
      Object.getPrototypeOf(Object.setPrototypeOf({}, null)) !== null &&
      Object.getPrototypeOf(Object.create(null)) === null) {
    (function () {
      var FAKENULL = Object.create(null);
      var gpo = Object.getPrototypeOf, spo = Object.setPrototypeOf;
      Object.getPrototypeOf = function (o) {
        var result = gpo(o);
        return result === FAKENULL ? null : result;
      };
      Object.setPrototypeOf = function (o, p) {
        var proto = p === null ? FAKENULL : p;
        return spo(o, proto);
      };
      Object.setPrototypeOf.polyfill = false;
    }());
  }

  var objectKeysAcceptsPrimitives = !throwsError(function () { Object.keys('foo'); });
  if (!objectKeysAcceptsPrimitives) {
    var originalObjectKeys = Object.keys;
    overrideNative(Object, 'keys', function keys(value) {
      return originalObjectKeys(ES.ToObject(value));
    });
  }

  if (Object.getOwnPropertyNames) {
    var objectGOPNAcceptsPrimitives = !throwsError(function () { Object.getOwnPropertyNames('foo'); });
    if (!objectGOPNAcceptsPrimitives) {
      var originalObjectGetOwnPropertyNames = Object.getOwnPropertyNames;
      overrideNative(Object, 'getOwnPropertyNames', function getOwnPropertyNames(value) {
        return originalObjectGetOwnPropertyNames(ES.ToObject(value));
      });
    }
  }
  if (Object.getOwnPropertyDescriptor) {
    var objectGOPDAcceptsPrimitives = !throwsError(function () { Object.getOwnPropertyDescriptor('foo', 'bar'); });
    if (!objectGOPDAcceptsPrimitives) {
      var originalObjectGetOwnPropertyDescriptor = Object.getOwnPropertyDescriptor;
      overrideNative(Object, 'getOwnPropertyDescriptor', function getOwnPropertyDescriptor(value, property) {
        return originalObjectGetOwnPropertyDescriptor(ES.ToObject(value), property);
      });
    }
  }
  if (Object.seal) {
    var objectSealAcceptsPrimitives = !throwsError(function () { Object.seal('foo'); });
    if (!objectSealAcceptsPrimitives) {
      var originalObjectSeal = Object.seal;
      overrideNative(Object, 'seal', function seal(value) {
        if (!Type.object(value)) { return value; }
        return originalObjectSeal(value);
      });
    }
  }
  if (Object.isSealed) {
    var objectIsSealedAcceptsPrimitives = !throwsError(function () { Object.isSealed('foo'); });
    if (!objectIsSealedAcceptsPrimitives) {
      var originalObjectIsSealed = Object.isSealed;
      overrideNative(Object, 'isSealed', function isSealed(value) {
        if (!Type.object(value)) { return true; }
        return originalObjectIsSealed(value);
      });
    }
  }
  if (Object.freeze) {
    var objectFreezeAcceptsPrimitives = !throwsError(function () { Object.freeze('foo'); });
    if (!objectFreezeAcceptsPrimitives) {
      var originalObjectFreeze = Object.freeze;
      overrideNative(Object, 'freeze', function freeze(value) {
        if (!Type.object(value)) { return value; }
        return originalObjectFreeze(value);
      });
    }
  }
  if (Object.isFrozen) {
    var objectIsFrozenAcceptsPrimitives = !throwsError(function () { Object.isFrozen('foo'); });
    if (!objectIsFrozenAcceptsPrimitives) {
      var originalObjectIsFrozen = Object.isFrozen;
      overrideNative(Object, 'isFrozen', function isFrozen(value) {
        if (!Type.object(value)) { return true; }
        return originalObjectIsFrozen(value);
      });
    }
  }
  if (Object.preventExtensions) {
    var objectPreventExtensionsAcceptsPrimitives = !throwsError(function () { Object.preventExtensions('foo'); });
    if (!objectPreventExtensionsAcceptsPrimitives) {
      var originalObjectPreventExtensions = Object.preventExtensions;
      overrideNative(Object, 'preventExtensions', function preventExtensions(value) {
        if (!Type.object(value)) { return value; }
        return originalObjectPreventExtensions(value);
      });
    }
  }
  if (Object.isExtensible) {
    var objectIsExtensibleAcceptsPrimitives = !throwsError(function () { Object.isExtensible('foo'); });
    if (!objectIsExtensibleAcceptsPrimitives) {
      var originalObjectIsExtensible = Object.isExtensible;
      overrideNative(Object, 'isExtensible', function isExtensible(value) {
        if (!Type.object(value)) { return false; }
        return originalObjectIsExtensible(value);
      });
    }
  }
  if (Object.getPrototypeOf) {
    var objectGetProtoAcceptsPrimitives = !throwsError(function () { Object.getPrototypeOf('foo'); });
    if (!objectGetProtoAcceptsPrimitives) {
      var originalGetProto = Object.getPrototypeOf;
      overrideNative(Object, 'getPrototypeOf', function getPrototypeOf(value) {
        return originalGetProto(ES.ToObject(value));
      });
    }
  }

  if (!RegExp.prototype.flags && supportsDescriptors) {
    var regExpFlagsGetter = function flags() {
      if (!ES.TypeIsObject(this)) {
        throw new TypeError('Method called on incompatible type: must be an object.');
      }
      var result = '';
      if (this.global) {
        result += 'g';
      }
      if (this.ignoreCase) {
        result += 'i';
      }
      if (this.multiline) {
        result += 'm';
      }
      if (this.unicode) {
        result += 'u';
      }
      if (this.sticky) {
        result += 'y';
      }
      return result;
    };

    Value.getter(RegExp.prototype, 'flags', regExpFlagsGetter);
  }

  var regExpSupportsFlagsWithRegex = valueOrFalseIfThrows(function () {
    return String(new RegExp(/a/g, 'i')) === '/a/i';
  });

  if (!regExpSupportsFlagsWithRegex && supportsDescriptors) {
    var OrigRegExp = RegExp;
    var RegExpShim = function RegExp(pattern, flags) {
      if (Type.regex(pattern) && Type.string(flags)) {
        return new RegExp(pattern.source, flags);
      }
      return new OrigRegExp(pattern, flags);
    };
    Value.preserveToString(RegExpShim, OrigRegExp);
    if (Object.setPrototypeOf) {
      // sets up proper prototype chain where possible
      Object.setPrototypeOf(OrigRegExp, RegExpShim);
    }
    Object.getOwnPropertyNames(OrigRegExp).forEach(function (key) {
      if (key === '$input') { return; } // Chrome < v39 & Opera < 26 have a nonstandard "$input" property
      if (key in noop) { return; }
      Value.proxy(OrigRegExp, key, RegExpShim);
    });
    RegExpShim.prototype = OrigRegExp.prototype;
    Value.redefine(OrigRegExp.prototype, 'constructor', RegExpShim);
    /*globals RegExp: true */
    RegExp = RegExpShim;
    Value.redefine(globals, 'RegExp', RegExpShim);
    /*globals RegExp: false */
  }

  if (supportsDescriptors) {
    var regexGlobals = {
      input: '$_',
      lastMatch: '$&',
      lastParen: '$+',
      leftContext: '$`',
      rightContext: '$\''
    };
    Object.keys(regexGlobals).forEach(function (prop) {
      if (prop in RegExp && !(regexGlobals[prop] in RegExp)) {
        Value.getter(RegExp, regexGlobals[prop], function get() {
          return RegExp[prop];
        });
      }
    });
  }

  var square = function (n) { return n * n; };
  var add = function (a, b) { return a + b; };
  var inverseEpsilon = 1 / Number.EPSILON;
  var roundTiesToEven = function roundTiesToEven(n) {
    // Even though this reduces down to `return n`, it takes advantage of built-in rounding.
    return (n + inverseEpsilon) - inverseEpsilon;
  };
  var BINARY_32_EPSILON = Math.pow(2, -23);
  var BINARY_32_MAX_VALUE = Math.pow(2, 127) * (2 - BINARY_32_EPSILON);
  var BINARY_32_MIN_VALUE = Math.pow(2, -126);

  var MathShims = {
    acosh: function acosh(value) {
      var x = Number(value);
      if (Number.isNaN(x) || value < 1) { return NaN; }
      if (x === 1) { return 0; }
      if (x === Infinity) { return x; }
      return Math.log(x / Math.E + Math.sqrt(x + 1) * Math.sqrt(x - 1) / Math.E) + 1;
    },

    asinh: function asinh(value) {
      var x = Number(value);
      if (x === 0 || !globalIsFinite(x)) {
        return x;
      }
      return x < 0 ? -Math.asinh(-x) : Math.log(x + Math.sqrt(x * x + 1));
    },

    atanh: function atanh(value) {
      var x = Number(value);
      if (Number.isNaN(x) || x < -1 || x > 1) {
        return NaN;
      }
      if (x === -1) { return -Infinity; }
      if (x === 1) { return Infinity; }
      if (x === 0) { return x; }
      return 0.5 * Math.log((1 + x) / (1 - x));
    },

    cbrt: function cbrt(value) {
      var x = Number(value);
      if (x === 0) { return x; }
      var negate = x < 0, result;
      if (negate) { x = -x; }
      if (x === Infinity) {
        result = Infinity;
      } else {
        result = Math.exp(Math.log(x) / 3);
        // from http://en.wikipedia.org/wiki/Cube_root#Numerical_methods
        result = (x / (result * result) + (2 * result)) / 3;
      }
      return negate ? -result : result;
    },

    clz32: function clz32(value) {
      // See https://bugs.ecmascript.org/show_bug.cgi?id=2465
      var x = Number(value);
      var number = ES.ToUint32(x);
      if (number === 0) {
        return 32;
      }
      return 31 - Math.floor(Math.log(number + 0.5) * Math.LOG2E);
    },

    cosh: function cosh(value) {
      var x = Number(value);
      if (x === 0) { return 1; } // +0 or -0
      if (Number.isNaN(x)) { return NaN; }
      if (!globalIsFinite(x)) { return Infinity; }
      if (x < 0) { x = -x; }
      if (x > 21) { return Math.exp(x) / 2; }
      return (Math.exp(x) + Math.exp(-x)) / 2;
    },

    expm1: function expm1(value) {
      var x = Number(value);
      if (x === -Infinity) { return -1; }
      if (!globalIsFinite(x) || x === 0) { return x; }
      if (Math.abs(x) > 0.5) {
        return Math.exp(x) - 1;
      }
      // A more precise approximation using Taylor series expansion
      // from https://github.com/paulmillr/es6-shim/issues/314#issuecomment-70293986
      var t = x;
      var sum = 0;
      var n = 1;
      while (sum + t !== sum) {
        sum += t;
        n += 1;
        t *= x / n;
      }
      return sum;
    },

    hypot: function hypot(x, y) {
      var anyNaN = false;
      var allZero = true;
      var anyInfinity = false;
      var numbers = [];
      Array.prototype.every.call(arguments, function (arg) {
        var num = Number(arg);
        if (Number.isNaN(num)) {
          anyNaN = true;
        } else if (num === Infinity || num === -Infinity) {
          anyInfinity = true;
        } else if (num !== 0) {
          allZero = false;
        }
        if (anyInfinity) {
          return false;
        } else if (!anyNaN) {
          numbers.push(Math.abs(num));
        }
        return true;
      });
      if (anyInfinity) { return Infinity; }
      if (anyNaN) { return NaN; }
      if (allZero) { return 0; }

      var largest = Math.max.apply(Math, numbers);
      var divided = numbers.map(function (number) { return number / largest; });
      var sum = divided.map(square).reduce(add);
      return largest * Math.sqrt(sum);
    },

    log2: function log2(value) {
      return Math.log(value) * Math.LOG2E;
    },

    log10: function log10(value) {
      return Math.log(value) * Math.LOG10E;
    },

    log1p: function log1p(value) {
      var x = Number(value);
      if (x < -1 || Number.isNaN(x)) { return NaN; }
      if (x === 0 || x === Infinity) { return x; }
      if (x === -1) { return -Infinity; }

      return (1 + x) - 1 === 0 ? x : x * (Math.log(1 + x) / ((1 + x) - 1));
    },

    sign: function sign(value) {
      var number = Number(value);
      if (number === 0) { return number; }
      if (Number.isNaN(number)) { return number; }
      return number < 0 ? -1 : 1;
    },

    sinh: function sinh(value) {
      var x = Number(value);
      if (!globalIsFinite(x) || x === 0) { return x; }

      if (Math.abs(x) < 1) {
        return (Math.expm1(x) - Math.expm1(-x)) / 2;
      }
      return (Math.exp(x - 1) - Math.exp(-x - 1)) * Math.E / 2;
    },

    tanh: function tanh(value) {
      var x = Number(value);
      if (Number.isNaN(x) || x === 0) { return x; }
      if (x === Infinity) { return 1; }
      if (x === -Infinity) { return -1; }
      var a = Math.expm1(x);
      var b = Math.expm1(-x);
      if (a === Infinity) { return 1; }
      if (b === Infinity) { return -1; }
      return (a - b) / (Math.exp(x) + Math.exp(-x));
    },

    trunc: function trunc(value) {
      var x = Number(value);
      return x < 0 ? -Math.floor(-x) : Math.floor(x);
    },

    imul: function imul(x, y) {
      // taken from https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Math/imul
      var a = ES.ToUint32(x);
      var b = ES.ToUint32(y);
      var ah = (a >>> 16) & 0xffff;
      var al = a & 0xffff;
      var bh = (b >>> 16) & 0xffff;
      var bl = b & 0xffff;
      // the shift by 0 fixes the sign on the high part
      // the final |0 converts the unsigned value into a signed value
      return ((al * bl) + (((ah * bl + al * bh) << 16) >>> 0) | 0);
    },

    fround: function fround(x) {
      var v = Number(x);
      if (v === 0 || v === Infinity || v === -Infinity || numberIsNaN(v)) {
        return v;
      }
      var sign = Math.sign(v);
      var abs = Math.abs(v);
      if (abs < BINARY_32_MIN_VALUE) {
        return sign * roundTiesToEven(abs / BINARY_32_MIN_VALUE / BINARY_32_EPSILON) * BINARY_32_MIN_VALUE * BINARY_32_EPSILON;
      }
      // Veltkamp's splitting (?)
      var a = (1 + BINARY_32_EPSILON / Number.EPSILON) * abs;
      var result = a - (a - abs);
      if (result > BINARY_32_MAX_VALUE || numberIsNaN(result)) {
        return sign * Infinity;
      }
      return sign * result;
    }
  };
  defineProperties(Math, MathShims);
  // IE 11 TP has an imprecise log1p: reports Math.log1p(-1e-17) as 0
  defineProperty(Math, 'log1p', MathShims.log1p, Math.log1p(-1e-17) !== -1e-17);
  // IE 11 TP has an imprecise asinh: reports Math.asinh(-1e7) as not exactly equal to -Math.asinh(1e7)
  defineProperty(Math, 'asinh', MathShims.asinh, Math.asinh(-1e7) !== -Math.asinh(1e7));
  // Chrome 40 has an imprecise Math.tanh with very small numbers
  defineProperty(Math, 'tanh', MathShims.tanh, Math.tanh(-2e-17) !== -2e-17);
  // Chrome 40 loses Math.acosh precision with high numbers
  defineProperty(Math, 'acosh', MathShims.acosh, Math.acosh(Number.MAX_VALUE) === Infinity);
  // Firefox 38 on Windows
  defineProperty(Math, 'cbrt', MathShims.cbrt, Math.abs(1 - Math.cbrt(1e-300) / 1e-100) / Number.EPSILON > 8);
  // node 0.11 has an imprecise Math.sinh with very small numbers
  defineProperty(Math, 'sinh', MathShims.sinh, Math.sinh(-2e-17) !== -2e-17);
  // FF 35 on Linux reports 22025.465794806725 for Math.expm1(10)
  var expm1OfTen = Math.expm1(10);
  defineProperty(Math, 'expm1', MathShims.expm1, expm1OfTen > 22025.465794806719 || expm1OfTen < 22025.4657948067165168);

  var origMathRound = Math.round;
  // breaks in e.g. Safari 8, Internet Explorer 11, Opera 12
  var roundHandlesBoundaryConditions = Math.round(0.5 - Number.EPSILON / 4) === 0 && Math.round(-0.5 + Number.EPSILON / 3.99) === 1;

  // When engines use Math.floor(x + 0.5) internally, Math.round can be buggy for large integers.
  // This behavior should be governed by "round to nearest, ties to even mode"
  // see https://people.mozilla.org/~jorendorff/es6-draft.html#sec-ecmascript-language-types-number-type
  // These are the boundary cases where it breaks.
  var smallestPositiveNumberWhereRoundBreaks = inverseEpsilon + 1;
  var largestPositiveNumberWhereRoundBreaks = 2 * inverseEpsilon - 1;
  var roundDoesNotIncreaseIntegers = [smallestPositiveNumberWhereRoundBreaks, largestPositiveNumberWhereRoundBreaks].every(function (num) {
    return Math.round(num) === num;
  });
  defineProperty(Math, 'round', function round(x) {
    var floor = Math.floor(x);
    var ceil = floor === -1 ? -0 : floor + 1;
    return x - floor < 0.5 ? floor : ceil;
  }, !roundHandlesBoundaryConditions || !roundDoesNotIncreaseIntegers);
  Value.preserveToString(Math.round, origMathRound);

  var origImul = Math.imul;
  if (Math.imul(0xffffffff, 5) !== -5) {
    // Safari 6.1, at least, reports "0" for this value
    Math.imul = MathShims.imul;
    Value.preserveToString(Math.imul, origImul);
  }
  if (Math.imul.length !== 2) {
    // Safari 8.0.4 has a length of 1
    // fixed in https://bugs.webkit.org/show_bug.cgi?id=143658
    overrideNative(Math, 'imul', function imul(x, y) {
      return origImul.apply(Math, arguments);
    });
  }

  // Promises
  // Simplest possible implementation; use a 3rd-party library if you
  // want the best possible speed and/or long stack traces.
  var PromiseShim = (function () {

    var Promise, Promise$prototype;

    ES.IsPromise = function (promise) {
      if (!ES.TypeIsObject(promise)) {
        return false;
      }
      if (!promise._promiseConstructor) {
        // _promiseConstructor is a bit more unique than _status, so we'll
        // check that instead of the [[PromiseStatus]] internal field.
        return false;
      }
      if (typeof promise._status === 'undefined') {
        return false; // uninitialized
      }
      return true;
    };

    // "PromiseCapability" in the spec is what most promise implementations
    // call a "deferred".
    var PromiseCapability = function (C) {
      if (!ES.IsCallable(C)) {
        throw new TypeError('bad promise constructor');
      }
      var capability = this;
      var resolver = function (resolve, reject) {
        capability.resolve = resolve;
        capability.reject = reject;
      };
      capability.promise = ES.Construct(C, [resolver]);
      // see https://bugs.ecmascript.org/show_bug.cgi?id=2478
      if (!capability.promise._es6construct) {
        throw new TypeError('bad promise constructor');
      }
      if (!(ES.IsCallable(capability.resolve) && ES.IsCallable(capability.reject))) {
        throw new TypeError('bad promise constructor');
      }
    };

    // find an appropriate setImmediate-alike
    var setTimeout = globals.setTimeout;
    var makeZeroTimeout;
    /*global window */
    if (typeof window !== 'undefined' && ES.IsCallable(window.postMessage)) {
      makeZeroTimeout = function () {
        // from http://dbaron.org/log/20100309-faster-timeouts
        var timeouts = [];
        var messageName = 'zero-timeout-message';
        var setZeroTimeout = function (fn) {
          timeouts.push(fn);
          window.postMessage(messageName, '*');
        };
        var handleMessage = function (event) {
          if (event.source === window && event.data === messageName) {
            event.stopPropagation();
            if (timeouts.length === 0) { return; }
            var fn = timeouts.shift();
            fn();
          }
        };
        window.addEventListener('message', handleMessage, true);
        return setZeroTimeout;
      };
    }
    var makePromiseAsap = function () {
      // An efficient task-scheduler based on a pre-existing Promise
      // implementation, which we can use even if we override the
      // global Promise below (in order to workaround bugs)
      // https://github.com/Raynos/observ-hash/issues/2#issuecomment-35857671
      var P = globals.Promise;
      return P && P.resolve && function (task) {
        return P.resolve().then(task);
      };
    };
    /*global process */
    var enqueue = ES.IsCallable(globals.setImmediate) ?
      globals.setImmediate.bind(globals) :
      typeof process === 'object' && process.nextTick ? process.nextTick :
      makePromiseAsap() ||
      (ES.IsCallable(makeZeroTimeout) ? makeZeroTimeout() :
      function (task) { setTimeout(task, 0); }); // fallback

    var updatePromiseFromPotentialThenable = function (x, capability) {
      if (!ES.TypeIsObject(x)) {
        return false;
      }
      var resolve = capability.resolve;
      var reject = capability.reject;
      try {
        var then = x.then; // only one invocation of accessor
        if (!ES.IsCallable(then)) { return false; }
        then.call(x, resolve, reject);
      } catch (e) {
        reject(e);
      }
      return true;
    };

    var triggerPromiseReactions = function (reactions, x) {
      reactions.forEach(function (reaction) {
        enqueue(function () {
          // PromiseReactionTask
          var handler = reaction.handler;
          var capability = reaction.capability;
          var resolve = capability.resolve;
          var reject = capability.reject;
          try {
            var result = handler(x);
            if (result === capability.promise) {
              throw new TypeError('self resolution');
            }
            var updateResult =
              updatePromiseFromPotentialThenable(result, capability);
            if (!updateResult) {
              resolve(result);
            }
          } catch (e) {
            reject(e);
          }
        });
      });
    };

    var promiseResolutionHandler = function (promise, onFulfilled, onRejected) {
      return function (x) {
        if (x === promise) {
          return onRejected(new TypeError('self resolution'));
        }
        var C = promise._promiseConstructor;
        var capability = new PromiseCapability(C);
        var updateResult = updatePromiseFromPotentialThenable(x, capability);
        if (updateResult) {
          return capability.promise.then(onFulfilled, onRejected);
        } else {
          return onFulfilled(x);
        }
      };
    };

    Promise = function (resolver) {
      var promise = this;
      promise = emulateES6construct(promise);
      if (!promise._promiseConstructor) {
        // we use _promiseConstructor as a stand-in for the internal
        // [[PromiseStatus]] field; it's a little more unique.
        throw new TypeError('bad promise');
      }
      if (typeof promise._status !== 'undefined') {
        throw new TypeError('promise already initialized');
      }
      // see https://bugs.ecmascript.org/show_bug.cgi?id=2482
      if (!ES.IsCallable(resolver)) {
        throw new TypeError('not a valid resolver');
      }
      promise._status = 'unresolved';
      promise._resolveReactions = [];
      promise._rejectReactions = [];

      var resolve = function (resolution) {
        if (promise._status !== 'unresolved') { return; }
        var reactions = promise._resolveReactions;
        promise._result = resolution;
        promise._resolveReactions = void 0;
        promise._rejectReactions = void 0;
        promise._status = 'has-resolution';
        triggerPromiseReactions(reactions, resolution);
      };
      var reject = function (reason) {
        if (promise._status !== 'unresolved') { return; }
        var reactions = promise._rejectReactions;
        promise._result = reason;
        promise._resolveReactions = void 0;
        promise._rejectReactions = void 0;
        promise._status = 'has-rejection';
        triggerPromiseReactions(reactions, reason);
      };
      try {
        resolver(resolve, reject);
      } catch (e) {
        reject(e);
      }
      return promise;
    };
    Promise$prototype = Promise.prototype;
    var _promiseAllResolver = function (index, values, capability, remaining) {
      var done = false;
      return function (x) {
        if (done) { return; } // protect against being called multiple times
        done = true;
        values[index] = x;
        if ((--remaining.count) === 0) {
          var resolve = capability.resolve;
          resolve(values); // call w/ this===undefined
        }
      };
    };

    defineProperty(Promise, symbolSpecies, function (obj) {
      var constructor = this;
      // AllocatePromise
      // The `obj` parameter is a hack we use for es5
      // compatibility.
      var prototype = constructor.prototype || Promise$prototype;
      var object = obj || create(prototype);
      defineProperties(object, {
        _status: void 0,
        _result: void 0,
        _resolveReactions: void 0,
        _rejectReactions: void 0,
        _promiseConstructor: void 0
      });
      object._promiseConstructor = constructor;
      return object;
    });
    defineProperties(Promise, {
      all: function all(iterable) {
        var C = this;
        var capability = new PromiseCapability(C);
        var resolve = capability.resolve;
        var reject = capability.reject;
        try {
          if (!ES.IsIterable(iterable)) {
            throw new TypeError('bad iterable');
          }
          var it = ES.GetIterator(iterable);
          var values = [], remaining = { count: 1 };
          for (var index = 0; ; index++) {
            var next = ES.IteratorNext(it);
            if (next.done) {
              break;
            }
            var nextPromise = C.resolve(next.value);
            var resolveElement = _promiseAllResolver(
              index, values, capability, remaining
            );
            remaining.count++;
            nextPromise.then(resolveElement, capability.reject);
          }
          if ((--remaining.count) === 0) {
            resolve(values); // call w/ this===undefined
          }
        } catch (e) {
          reject(e);
        }
        return capability.promise;
      },

      race: function race(iterable) {
        var C = this;
        var capability = new PromiseCapability(C);
        var resolve = capability.resolve;
        var reject = capability.reject;
        try {
          if (!ES.IsIterable(iterable)) {
            throw new TypeError('bad iterable');
          }
          var it = ES.GetIterator(iterable);
          while (true) {
            var next = ES.IteratorNext(it);
            if (next.done) {
              // If iterable has no items, resulting promise will never
              // resolve; see:
              // https://github.com/domenic/promises-unwrapping/issues/75
              // https://bugs.ecmascript.org/show_bug.cgi?id=2515
              break;
            }
            var nextPromise = C.resolve(next.value);
            nextPromise.then(resolve, reject);
          }
        } catch (e) {
          reject(e);
        }
        return capability.promise;
      },

      reject: function reject(reason) {
        var C = this;
        var capability = new PromiseCapability(C);
        var rejectPromise = capability.reject;
        rejectPromise(reason); // call with this===undefined
        return capability.promise;
      },

      resolve: function resolve(v) {
        var C = this;
        if (ES.IsPromise(v)) {
          var constructor = v._promiseConstructor;
          if (constructor === C) { return v; }
        }
        var capability = new PromiseCapability(C);
        var resolvePromise = capability.resolve;
        resolvePromise(v); // call with this===undefined
        return capability.promise;
      }
    });

    var Identity = function (x) { return x; };
    var Thrower = function (e) { throw e; };

    defineProperties(Promise$prototype, {
      'catch': function (onRejected) {
        return this.then(void 0, onRejected);
      },

      then: function then(onFulfilled, onRejected) {
        var promise = this;
        if (!ES.IsPromise(promise)) { throw new TypeError('not a promise'); }
        // this.constructor not this._promiseConstructor; see
        // https://bugs.ecmascript.org/show_bug.cgi?id=2513
        var C = this.constructor;
        var capability = new PromiseCapability(C);
        if (!ES.IsCallable(onRejected)) {
          onRejected = Thrower;
        }
        if (!ES.IsCallable(onFulfilled)) {
          onFulfilled = Identity;
        }
        var resolutionHandler = promiseResolutionHandler(promise, onFulfilled, onRejected);
        var resolveReaction = { capability: capability, handler: resolutionHandler };
        var rejectReaction = { capability: capability, handler: onRejected };
        switch (promise._status) {
          case 'unresolved':
            promise._resolveReactions.push(resolveReaction);
            promise._rejectReactions.push(rejectReaction);
            break;
          case 'has-resolution':
            triggerPromiseReactions([resolveReaction], promise._result);
            break;
          case 'has-rejection':
            triggerPromiseReactions([rejectReaction], promise._result);
            break;
          default:
            throw new TypeError('unexpected');
        }
        return capability.promise;
      }
    });

    return Promise;
  }());

  // Chrome's native Promise has extra methods that it shouldn't have. Let's remove them.
  if (globals.Promise) {
    delete globals.Promise.accept;
    delete globals.Promise.defer;
    delete globals.Promise.prototype.chain;
  }

  // export the Promise constructor.
  defineProperties(globals, { Promise: PromiseShim });
  // In Chrome 33 (and thereabouts) Promise is defined, but the
  // implementation is buggy in a number of ways.  Let's check subclassing
  // support to see if we have a buggy implementation.
  var promiseSupportsSubclassing = supportsSubclassing(globals.Promise, function (S) {
    return S.resolve(42) instanceof S;
  });
  var promiseIgnoresNonFunctionThenCallbacks = !throwsError(function () { globals.Promise.reject(42).then(null, 5).then(null, noop); });
  var promiseRequiresObjectContext = throwsError(function () { globals.Promise.call(3, noop); });
  if (!promiseSupportsSubclassing || !promiseIgnoresNonFunctionThenCallbacks || !promiseRequiresObjectContext) {
    /*globals Promise: true */
    Promise = PromiseShim;
    /*globals Promise: false */
    overrideNative(globals, 'Promise', PromiseShim);
  }

  // Map and Set require a true ES5 environment
  // Their fast path also requires that the environment preserve
  // property insertion order, which is not guaranteed by the spec.
  var testOrder = function (a) {
    var b = Object.keys(a.reduce(function (o, k) {
      o[k] = true;
      return o;
    }, {}));
    return a.join(':') === b.join(':');
  };
  var preservesInsertionOrder = testOrder(['z', 'a', 'bb']);
  // some engines (eg, Chrome) only preserve insertion order for string keys
  var preservesNumericInsertionOrder = testOrder(['z', 1, 'a', '3', 2]);

  if (supportsDescriptors) {

    var fastkey = function fastkey(key) {
      if (!preservesInsertionOrder) {
        return null;
      }
      var type = typeof key;
      if (type === 'string') {
        return '$' + key;
      } else if (type === 'number') {
        // note that -0 will get coerced to "0" when used as a property key
        if (!preservesNumericInsertionOrder) {
          return 'n' + key;
        }
        return key;
      }
      return null;
    };

    var emptyObject = function emptyObject() {
      // accomodate some older not-quite-ES5 browsers
      return Object.create ? Object.create(null) : {};
    };

    var collectionShims = {
      Map: (function () {

        var empty = {};

        function MapEntry(key, value) {
          this.key = key;
          this.value = value;
          this.next = null;
          this.prev = null;
        }

        MapEntry.prototype.isRemoved = function () {
          return this.key === empty;
        };

        function MapIterator(map, kind) {
          this.head = map._head;
          this.i = this.head;
          this.kind = kind;
        }

        MapIterator.prototype = {
          next: function () {
            var i = this.i, kind = this.kind, head = this.head, result;
            if (typeof this.i === 'undefined') {
              return { value: void 0, done: true };
            }
            while (i.isRemoved() && i !== head) {
              // back up off of removed entries
              i = i.prev;
            }
            // advance to next unreturned element.
            while (i.next !== head) {
              i = i.next;
              if (!i.isRemoved()) {
                if (kind === 'key') {
                  result = i.key;
                } else if (kind === 'value') {
                  result = i.value;
                } else {
                  result = [i.key, i.value];
                }
                this.i = i;
                return { value: result, done: false };
              }
            }
            // once the iterator is done, it is done forever.
            this.i = void 0;
            return { value: void 0, done: true };
          }
        };
        addIterator(MapIterator.prototype);

        function Map() {
          var map = this;
          if (!ES.TypeIsObject(map)) {
            throw new TypeError("Constructor Map requires 'new'");
          }
          map = emulateES6construct(map);
          if (!map._es6map) {
            throw new TypeError('bad map');
          }

          var head = new MapEntry(null, null);
          // circular doubly-linked list.
          head.next = head.prev = head;

          defineProperties(map, {
            _head: head,
            _storage: emptyObject(),
            _size: 0
          });

          // Optionally initialize map from iterable
          if (arguments.length > 0 && typeof arguments[0] !== 'undefined' && arguments[0] !== null) {
            var it = ES.GetIterator(arguments[0]);
            var adder = map.set;
            if (!ES.IsCallable(adder)) { throw new TypeError('bad map'); }
            while (true) {
              var next = ES.IteratorNext(it);
              if (next.done) { break; }
              var nextItem = next.value;
              if (!ES.TypeIsObject(nextItem)) {
                throw new TypeError('expected iterable of pairs');
              }
              adder.call(map, nextItem[0], nextItem[1]);
            }
          }
          return map;
        }
        var Map$prototype = Map.prototype;
        defineProperty(Map, symbolSpecies, function (obj) {
          var constructor = this;
          var prototype = constructor.prototype || Map$prototype;
          var object = obj || create(prototype);
          defineProperties(object, { _es6map: true });
          return object;
        });

        Value.getter(Map.prototype, 'size', function () {
          if (typeof this._size === 'undefined') {
            throw new TypeError('size method called on incompatible Map');
          }
          return this._size;
        });

        defineProperties(Map.prototype, {
          get: function (key) {
            var fkey = fastkey(key);
            if (fkey !== null) {
              // fast O(1) path
              var entry = this._storage[fkey];
              if (entry) {
                return entry.value;
              } else {
                return;
              }
            }
            var head = this._head, i = head;
            while ((i = i.next) !== head) {
              if (ES.SameValueZero(i.key, key)) {
                return i.value;
              }
            }
          },

          has: function (key) {
            var fkey = fastkey(key);
            if (fkey !== null) {
              // fast O(1) path
              return typeof this._storage[fkey] !== 'undefined';
            }
            var head = this._head, i = head;
            while ((i = i.next) !== head) {
              if (ES.SameValueZero(i.key, key)) {
                return true;
              }
            }
            return false;
          },

          set: function (key, value) {
            var head = this._head, i = head, entry;
            var fkey = fastkey(key);
            if (fkey !== null) {
              // fast O(1) path
              if (typeof this._storage[fkey] !== 'undefined') {
                this._storage[fkey].value = value;
                return this;
              } else {
                entry = this._storage[fkey] = new MapEntry(key, value);
                i = head.prev;
                // fall through
              }
            }
            while ((i = i.next) !== head) {
              if (ES.SameValueZero(i.key, key)) {
                i.value = value;
                return this;
              }
            }
            entry = entry || new MapEntry(key, value);
            if (ES.SameValue(-0, key)) {
              entry.key = +0; // coerce -0 to +0 in entry
            }
            entry.next = this._head;
            entry.prev = this._head.prev;
            entry.prev.next = entry;
            entry.next.prev = entry;
            this._size += 1;
            return this;
          },

          'delete': function (key) {
            var head = this._head, i = head;
            var fkey = fastkey(key);
            if (fkey !== null) {
              // fast O(1) path
              if (typeof this._storage[fkey] === 'undefined') {
                return false;
              }
              i = this._storage[fkey].prev;
              delete this._storage[fkey];
              // fall through
            }
            while ((i = i.next) !== head) {
              if (ES.SameValueZero(i.key, key)) {
                i.key = i.value = empty;
                i.prev.next = i.next;
                i.next.prev = i.prev;
                this._size -= 1;
                return true;
              }
            }
            return false;
          },

          clear: function clear() {
            this._size = 0;
            this._storage = emptyObject();
            var head = this._head, i = head, p = i.next;
            while ((i = p) !== head) {
              i.key = i.value = empty;
              p = i.next;
              i.next = i.prev = head;
            }
            head.next = head.prev = head;
          },

          keys: function keys() {
            return new MapIterator(this, 'key');
          },

          values: function values() {
            return new MapIterator(this, 'value');
          },

          entries: function entries() {
            return new MapIterator(this, 'key+value');
          },

          forEach: function forEach(callback) {
            var context = arguments.length > 1 ? arguments[1] : null;
            var it = this.entries();
            for (var entry = it.next(); !entry.done; entry = it.next()) {
              if (context) {
                callback.call(context, entry.value[1], entry.value[0], this);
              } else {
                callback(entry.value[1], entry.value[0], this);
              }
            }
          }
        });
        addIterator(Map.prototype, function () { return this.entries(); });

        return Map;
      }()),

      Set: (function () {
        // Creating a Map is expensive.  To speed up the common case of
        // Sets containing only string or numeric keys, we use an object
        // as backing storage and lazily create a full Map only when
        // required.
        var SetShim = function Set() {
          var set = this;
          if (!ES.TypeIsObject(set)) {
            throw new TypeError("Constructor Set requires 'new'");
          }
          set = emulateES6construct(set);
          if (!set._es6set) {
            throw new TypeError('bad set');
          }

          defineProperties(set, {
            '[[SetData]]': null,
            _storage: emptyObject()
          });

          // Optionally initialize map from iterable
          if (arguments.length > 0 && typeof arguments[0] !== 'undefined' && arguments[0] !== null) {
            var it = ES.GetIterator(arguments[0]);
            var adder = set.add;
            if (!ES.IsCallable(adder)) { throw new TypeError('bad set'); }
            while (true) {
              var next = ES.IteratorNext(it);
              if (next.done) { break; }
              var nextItem = next.value;
              adder.call(set, nextItem);
            }
          }
          return set;
        };
        var Set$prototype = SetShim.prototype;
        defineProperty(SetShim, symbolSpecies, function (obj) {
          var constructor = this;
          var prototype = constructor.prototype || Set$prototype;
          var object = obj || create(prototype);
          defineProperties(object, { _es6set: true });
          return object;
        });

        // Switch from the object backing storage to a full Map.
        var ensureMap = function ensureMap(set) {
          if (!set['[[SetData]]']) {
            var m = set['[[SetData]]'] = new collectionShims.Map();
            Object.keys(set._storage).forEach(function (k) {
              // fast check for leading '$'
              if (k.charCodeAt(0) === 36) {
                k = k.slice(1);
              } else if (k.charAt(0) === 'n') {
                k = +k.slice(1);
              } else {
                k = +k;
              }
              m.set(k, k);
            });
            set._storage = null; // free old backing storage
          }
        };

        Value.getter(SetShim.prototype, 'size', function () {
          if (typeof this._storage === 'undefined') {
            // https://github.com/paulmillr/es6-shim/issues/176
            throw new TypeError('size method called on incompatible Set');
          }
          ensureMap(this);
          return this['[[SetData]]'].size;
        });

        defineProperties(SetShim.prototype, {
          has: function (key) {
            var fkey;
            if (this._storage && (fkey = fastkey(key)) !== null) {
              return !!this._storage[fkey];
            }
            ensureMap(this);
            return this['[[SetData]]'].has(key);
          },

          add: function (key) {
            var fkey;
            if (this._storage && (fkey = fastkey(key)) !== null) {
              this._storage[fkey] = true;
              return this;
            }
            ensureMap(this);
            this['[[SetData]]'].set(key, key);
            return this;
          },

          'delete': function (key) {
            var fkey;
            if (this._storage && (fkey = fastkey(key)) !== null) {
              var hasFKey = _hasOwnProperty(this._storage, fkey);
              return (delete this._storage[fkey]) && hasFKey;
            }
            ensureMap(this);
            return this['[[SetData]]']['delete'](key);
          },

          clear: function clear() {
            if (this._storage) {
              this._storage = emptyObject();
            } else {
              this['[[SetData]]'].clear();
            }
          },

          values: function values() {
            ensureMap(this);
            return this['[[SetData]]'].values();
          },

          entries: function entries() {
            ensureMap(this);
            return this['[[SetData]]'].entries();
          },

          forEach: function forEach(callback) {
            var context = arguments.length > 1 ? arguments[1] : null;
            var entireSet = this;
            ensureMap(entireSet);
            this['[[SetData]]'].forEach(function (value, key) {
              if (context) {
                callback.call(context, key, key, entireSet);
              } else {
                callback(key, key, entireSet);
              }
            });
          }
        });
        defineProperty(SetShim, 'keys', SetShim.values, true);
        addIterator(SetShim.prototype, function () { return this.values(); });

        return SetShim;
      }())
    };
    defineProperties(globals, collectionShims);

    if (globals.Map || globals.Set) {
      // Safari 8, for example, doesn't accept an iterable.
      var mapAcceptsArguments = valueOrFalseIfThrows(function () { return new Map([[1, 2]]).get(1) === 2; });
      if (!mapAcceptsArguments) {
        var OrigMapNoArgs = globals.Map;
        globals.Map = function Map() {
          if (!(this instanceof Map)) {
            throw new TypeError('Constructor Map requires "new"');
          }
          var m = new OrigMapNoArgs();
          var iterable;
          if (arguments.length > 0) {
            iterable = arguments[0];
          }
          if (Array.isArray(iterable) || Type.string(iterable)) {
            Array.prototype.forEach.call(iterable, function (entry) {
              m.set(entry[0], entry[1]);
            });
          } else if (iterable instanceof Map) {
            Map.prototype.forEach.call(iterable, function (value, key) {
              m.set(key, value);
            });
          }
          Object.setPrototypeOf(m, globals.Map.prototype);
          defineProperty(m, 'constructor', Map, true);
          return m;
        };
        globals.Map.prototype = create(OrigMapNoArgs.prototype);
        Value.preserveToString(globals.Map, OrigMapNoArgs);
      }
      var m = new Map();
      var mapUsesSameValueZero = (function (m) {
        m['delete'](0);
        m['delete'](-0);
        m.set(0, 3);
        m.get(-0, 4);
        return m.get(0) === 3 && m.get(-0) === 4;
      }(m));
      var mapSupportsChaining = m.set(1, 2) === m;
      if (!mapUsesSameValueZero || !mapSupportsChaining) {
        var origMapSet = Map.prototype.set;
        overrideNative(Map.prototype, 'set', function set(k, v) {
          origMapSet.call(this, k === 0 ? 0 : k, v);
          return this;
        });
      }
      if (!mapUsesSameValueZero) {
        var origMapGet = Map.prototype.get;
        var origMapHas = Map.prototype.has;
        defineProperties(Map.prototype, {
          get: function get(k) {
            return origMapGet.call(this, k === 0 ? 0 : k);
          },
          has: function has(k) {
            return origMapHas.call(this, k === 0 ? 0 : k);
          }
        }, true);
        Value.preserveToString(Map.prototype.get, origMapGet);
        Value.preserveToString(Map.prototype.has, origMapHas);
      }
      var s = new Set();
      var setUsesSameValueZero = (function (s) {
        s['delete'](0);
        s.add(-0);
        return !s.has(0);
      }(s));
      var setSupportsChaining = s.add(1) === s;
      if (!setUsesSameValueZero || !setSupportsChaining) {
        var origSetAdd = Set.prototype.add;
        Set.prototype.add = function add(v) {
          origSetAdd.call(this, v === 0 ? 0 : v);
          return this;
        };
        Value.preserveToString(Set.prototype.add, origSetAdd);
      }
      if (!setUsesSameValueZero) {
        var origSetHas = Set.prototype.has;
        Set.prototype.has = function has(v) {
          return origSetHas.call(this, v === 0 ? 0 : v);
        };
        Value.preserveToString(Set.prototype.has, origSetHas);
        var origSetDel = Set.prototype['delete'];
        Set.prototype['delete'] = function SetDelete(v) {
          return origSetDel.call(this, v === 0 ? 0 : v);
        };
        Value.preserveToString(Set.prototype['delete'], origSetDel);
      }
      var mapSupportsSubclassing = supportsSubclassing(globals.Map, function (M) {
        var m = new M([]);
        // Firefox 32 is ok with the instantiating the subclass but will
        // throw when the map is used.
        m.set(42, 42);
        return m instanceof M;
      });
      var mapFailsToSupportSubclassing = Object.setPrototypeOf && !mapSupportsSubclassing; // without Object.setPrototypeOf, subclassing is not possible
      var mapRequiresNew = (function () {
        try {
          return !(globals.Map() instanceof globals.Map);
        } catch (e) {
          return e instanceof TypeError;
        }
      }());
      if (globals.Map.length !== 0 || mapFailsToSupportSubclassing || !mapRequiresNew) {
        var OrigMap = globals.Map;
        globals.Map = function Map() {
          if (!(this instanceof Map)) {
            throw new TypeError('Constructor Map requires "new"');
          }
          var m = arguments.length > 0 ? new OrigMap(arguments[0]) : new OrigMap();
          Object.setPrototypeOf(m, Map.prototype);
          defineProperty(m, 'constructor', Map, true);
          return m;
        };
        globals.Map.prototype = create(OrigMap.prototype);
        Value.preserveToString(globals.Map, OrigMap);
      }
      var setSupportsSubclassing = supportsSubclassing(globals.Set, function (S) {
        var s = new S([]);
        s.add(42, 42);
        return s instanceof S;
      });
      var setFailsToSupportSubclassing = Object.setPrototypeOf && !setSupportsSubclassing; // without Object.setPrototypeOf, subclassing is not possible
      var setRequiresNew = (function () {
        try {
          return !(globals.Set() instanceof globals.Set);
        } catch (e) {
          return e instanceof TypeError;
        }
      }());
      if (globals.Set.length !== 0 || setFailsToSupportSubclassing || !setRequiresNew) {
        var OrigSet = globals.Set;
        globals.Set = function Set() {
          if (!(this instanceof Set)) {
            throw new TypeError('Constructor Set requires "new"');
          }
          var s = arguments.length > 0 ? new OrigSet(arguments[0]) : new OrigSet();
          Object.setPrototypeOf(s, Set.prototype);
          defineProperty(s, 'constructor', Set, true);
          return s;
        };
        globals.Set.prototype = create(OrigSet.prototype);
        Value.preserveToString(globals.Set, OrigSet);
      }
      var mapIterationThrowsStopIterator = !valueOrFalseIfThrows(function () {
        return (new Map()).keys().next().done;
      });
      /*
        - In Firefox < 23, Map#size is a function.
        - In all current Firefox, Set#entries/keys/values & Map#clear do not exist
        - https://bugzilla.mozilla.org/show_bug.cgi?id=869996
        - In Firefox 24, Map and Set do not implement forEach
        - In Firefox 25 at least, Map and Set are callable without "new"
      */
      if (
        typeof globals.Map.prototype.clear !== 'function' ||
        new globals.Set().size !== 0 ||
        new globals.Map().size !== 0 ||
        typeof globals.Map.prototype.keys !== 'function' ||
        typeof globals.Set.prototype.keys !== 'function' ||
        typeof globals.Map.prototype.forEach !== 'function' ||
        typeof globals.Set.prototype.forEach !== 'function' ||
        isCallableWithoutNew(globals.Map) ||
        isCallableWithoutNew(globals.Set) ||
        typeof (new globals.Map().keys().next) !== 'function' || // Safari 8
        mapIterationThrowsStopIterator || // Firefox 25
        !mapSupportsSubclassing
      ) {
        delete globals.Map; // necessary to overwrite in Safari 8
        delete globals.Set; // necessary to overwrite in Safari 8
        defineProperties(globals, {
          Map: collectionShims.Map,
          Set: collectionShims.Set
        }, true);
      }
    }
    if (globals.Set.prototype.keys !== globals.Set.prototype.values) {
      defineProperty(globals.Set.prototype, 'keys', globals.Set.prototype.values, true);
    }
    // Shim incomplete iterator implementations.
    addIterator(Object.getPrototypeOf((new globals.Map()).keys()));
    addIterator(Object.getPrototypeOf((new globals.Set()).keys()));
  }

  // Reflect
  if (!globals.Reflect) {
    defineProperty(globals, 'Reflect', {});
  }
  var Reflect = globals.Reflect;

  var throwUnlessTargetIsObject = function throwUnlessTargetIsObject(target) {
    if (!ES.TypeIsObject(target)) {
      throw new TypeError('target must be an object');
    }
  };

  // Some Reflect methods are basically the same as
  // those on the Object global, except that a TypeError is thrown if
  // target isn't an object. As well as returning a boolean indicating
  // the success of the operation.
  defineProperties(globals.Reflect, {
    // Apply method in a functional form.
    apply: function apply() {
      return ES.Call.apply(null, arguments);
    },

    // New operator in a functional form.
    construct: function construct(constructor, args) {
      if (!ES.IsCallable(constructor)) {
        throw new TypeError('First argument must be callable.');
      }

      return ES.Construct(constructor, args);
    },

    // When deleting a non-existent or configurable property,
    // true is returned.
    // When attempting to delete a non-configurable property,
    // it will return false.
    deleteProperty: function deleteProperty(target, key) {
      throwUnlessTargetIsObject(target);
      if (supportsDescriptors) {
        var desc = Object.getOwnPropertyDescriptor(target, key);

        if (desc && !desc.configurable) {
          return false;
        }
      }

      // Will return true.
      return delete target[key];
    },

    enumerate: function enumerate(target) {
      throwUnlessTargetIsObject(target);
      return new ObjectIterator(target, 'key');
    },

    has: function has(target, key) {
      throwUnlessTargetIsObject(target);
      return key in target;
    }
  });

  if (Object.getOwnPropertyNames) {
    defineProperties(globals.Reflect, {
      // Basically the result of calling the internal [[OwnPropertyKeys]].
      // Concatenating propertyNames and propertySymbols should do the trick.
      // This should continue to work together with a Symbol shim
      // which overrides Object.getOwnPropertyNames and implements
      // Object.getOwnPropertySymbols.
      ownKeys: function ownKeys(target) {
        throwUnlessTargetIsObject(target);
        var keys = Object.getOwnPropertyNames(target);

        if (ES.IsCallable(Object.getOwnPropertySymbols)) {
          keys.push.apply(keys, Object.getOwnPropertySymbols(target));
        }

        return keys;
      }
    });
  }

  var callAndCatchException = function ConvertExceptionToBoolean(func) {
    return !throwsError(func);
  };

  if (Object.preventExtensions) {
    defineProperties(globals.Reflect, {
      isExtensible: function isExtensible(target) {
        throwUnlessTargetIsObject(target);
        return Object.isExtensible(target);
      },
      preventExtensions: function preventExtensions(target) {
        throwUnlessTargetIsObject(target);
        return callAndCatchException(function () {
          Object.preventExtensions(target);
        });
      }
    });
  }

  if (supportsDescriptors) {
    var internalGet = function get(target, key, receiver) {
      var desc = Object.getOwnPropertyDescriptor(target, key);

      if (!desc) {
        var parent = Object.getPrototypeOf(target);

        if (parent === null) {
          return undefined;
        }

        return internalGet(parent, key, receiver);
      }

      if ('value' in desc) {
        return desc.value;
      }

      if (desc.get) {
        return desc.get.call(receiver);
      }

      return undefined;
    };

    var internalSet = function set(target, key, value, receiver) {
      var desc = Object.getOwnPropertyDescriptor(target, key);

      if (!desc) {
        var parent = Object.getPrototypeOf(target);

        if (parent !== null) {
          return internalSet(parent, key, value, receiver);
        }

        desc = {
          value: void 0,
          writable: true,
          enumerable: true,
          configurable: true
        };
      }

      if ('value' in desc) {
        if (!desc.writable) {
          return false;
        }

        if (!ES.TypeIsObject(receiver)) {
          return false;
        }

        var existingDesc = Object.getOwnPropertyDescriptor(receiver, key);

        if (existingDesc) {
          return Reflect.defineProperty(receiver, key, {
            value: value
          });
        } else {
          return Reflect.defineProperty(receiver, key, {
            value: value,
            writable: true,
            enumerable: true,
            configurable: true
          });
        }
      }

      if (desc.set) {
        desc.set.call(receiver, value);
        return true;
      }

      return false;
    };

    defineProperties(globals.Reflect, {
      defineProperty: function defineProperty(target, propertyKey, attributes) {
        throwUnlessTargetIsObject(target);
        return callAndCatchException(function () {
          Object.defineProperty(target, propertyKey, attributes);
        });
      },

      getOwnPropertyDescriptor: function getOwnPropertyDescriptor(target, propertyKey) {
        throwUnlessTargetIsObject(target);
        return Object.getOwnPropertyDescriptor(target, propertyKey);
      },

      // Syntax in a functional form.
      get: function get(target, key) {
        throwUnlessTargetIsObject(target);
        var receiver = arguments.length > 2 ? arguments[2] : target;

        return internalGet(target, key, receiver);
      },

      set: function set(target, key, value) {
        throwUnlessTargetIsObject(target);
        var receiver = arguments.length > 3 ? arguments[3] : target;

        return internalSet(target, key, value, receiver);
      }
    });
  }

  if (Object.getPrototypeOf) {
    var objectDotGetPrototypeOf = Object.getPrototypeOf;
    defineProperties(globals.Reflect, {
      getPrototypeOf: function getPrototypeOf(target) {
        throwUnlessTargetIsObject(target);
        return objectDotGetPrototypeOf(target);
      }
    });
  }

  if (Object.setPrototypeOf) {
    var willCreateCircularPrototype = function (object, proto) {
      while (proto) {
        if (object === proto) {
          return true;
        }
        proto = Reflect.getPrototypeOf(proto);
      }
      return false;
    };

    defineProperties(globals.Reflect, {
      // Sets the prototype of the given object.
      // Returns true on success, otherwise false.
      setPrototypeOf: function setPrototypeOf(object, proto) {
        throwUnlessTargetIsObject(object);
        if (proto !== null && !ES.TypeIsObject(proto)) {
          throw new TypeError('proto must be an object or null');
        }

        // If they already are the same, we're done.
        if (proto === Reflect.getPrototypeOf(object)) {
          return true;
        }

        // Cannot alter prototype if object not extensible.
        if (Reflect.isExtensible && !Reflect.isExtensible(object)) {
          return false;
        }

        // Ensure that we do not create a circular prototype chain.
        if (willCreateCircularPrototype(object, proto)) {
          return false;
        }

        Object.setPrototypeOf(object, proto);

        return true;
      }
    });
  }

  if (String(new Date(NaN)) !== 'Invalid Date') {
    var dateToString = Date.prototype.toString;
    var shimmedDateToString = function toString() {
      var valueOf = +this;
      if (valueOf !== valueOf) {
        return 'Invalid Date';
      }
      return dateToString.call(this);
    };
    overrideNative(Date.prototype, 'toString', shimmedDateToString);
  }

  // Annex B HTML methods
  // https://people.mozilla.org/~jorendorff/es6-draft.html#sec-additional-properties-of-the-string.prototype-object
  var stringHTMLshims = {
    anchor: function anchor(name) { return ES.CreateHTML(this, 'a', 'name', name); },
    big: function big() { return ES.CreateHTML(this, 'big', '', ''); },
    blink: function blink() { return ES.CreateHTML(this, 'blink', '', ''); },
    bold: function bold() { return ES.CreateHTML(this, 'b', '', ''); },
    fixed: function fixed() { return ES.CreateHTML(this, 'tt', '', ''); },
    fontcolor: function fontcolor(color) { return ES.CreateHTML(this, 'font', 'color', color); },
    fontsize: function fontsize(size) { return ES.CreateHTML(this, 'font', 'size', size); },
    italics: function italics() { return ES.CreateHTML(this, 'i', '', ''); },
    link: function link(url) { return ES.CreateHTML(this, 'a', 'href', url); },
    small: function small() { return ES.CreateHTML(this, 'small', '', ''); },
    strike: function strike() { return ES.CreateHTML(this, 'strike', '', ''); },
    sub: function sub() { return ES.CreateHTML(this, 'sub', '', ''); },
    sup: function sub() { return ES.CreateHTML(this, 'sup', '', ''); }
  };
  defineProperties(String.prototype, stringHTMLshims);
  Object.keys(stringHTMLshims).forEach(function (key) {
    var method = String.prototype[key];
    var shouldOverwrite = false;
    if (ES.IsCallable(method)) {
      var output = method.call('', ' " ');
      var quotesCount = [].concat(output.match(/"/g)).length;
      shouldOverwrite = output !== output.toLowerCase() || quotesCount > 2;
    } else {
      shouldOverwrite = true;
    }
    if (shouldOverwrite) {
      defineProperty(String.prototype, key, stringHTMLshims[key], true);
    }
  });

  return globals;
}));
;
 /*!
  * https://github.com/paulmillr/es6-shim
  * @license es6-shim Copyright 2013-2015 by Paul Miller (http://paulmillr.com)
  *   and contributors,  MIT License
  * es6-sham: v0.27.1
  * see https://github.com/paulmillr/es6-shim/blob/0.27.1/LICENSE
  * Details and documentation:
  * https://github.com/paulmillr/es6-shim/
  */

// UMD (Universal Module Definition)
// see https://github.com/umdjs/umd/blob/master/returnExports.js
(function (root, factory) {
  /*global define, exports, module */
  if (typeof define === 'function' && define.amd) {
    // AMD. Register as an anonymous module.
    define(factory);
  } else if (typeof exports === 'object') {
    // Node. Does not work with strict CommonJS, but
    // only CommonJS-like enviroments that support module.exports,
    // like Node.
    module.exports = factory();
  } else {
    // Browser globals (root is window)
    root.returnExports = factory();
  }
}(this, function () {
  'use strict';

  /*jshint evil: true */
  var getGlobal = new Function('return this;');
  /*jshint evil: false */

  var globals = getGlobal();
  var Object = globals.Object;

  // NOTE:  This versions needs object ownership
  //        beacuse every promoted object needs to be reassigned
  //        otherwise uncompatible browsers cannot work as expected
  //
  // NOTE:  This might need es5-shim or polyfills upfront
  //        because it's based on ES5 API.
  //        (probably just an IE <= 8 problem)
  //
  // NOTE:  nodejs is fine in version 0.8, 0.10, and future versions.
  (function () {
    if (Object.setPrototypeOf) { return; }

    /*jshint proto: true */
    // @author    Andrea Giammarchi - @WebReflection

    var getOwnPropertyNames = Object.getOwnPropertyNames;
    var getOwnPropertyDescriptor = Object.getOwnPropertyDescriptor;
    var create = Object.create;
    var defineProperty = Object.defineProperty;
    var getPrototypeOf = Object.getPrototypeOf;
    var objProto = Object.prototype;

    var copyDescriptors = function (target, source) {
      // define into target descriptors from source
      getOwnPropertyNames(source).forEach(function (key) {
        defineProperty(
          target,
          key,
          getOwnPropertyDescriptor(source, key)
        );
      });
      return target;
    };
    // used as fallback when no promotion is possible
    var createAndCopy = function (origin, proto) {
      return copyDescriptors(create(proto), origin);
    };
    var set, setPrototypeOf;
    try {
      // this might fail for various reasons
      // ignore if Chrome cought it at runtime
      set = getOwnPropertyDescriptor(objProto, '__proto__').set;
      set.call({}, null);
      // setter not poisoned, it can promote
      // Firefox, Chrome
      setPrototypeOf = function (origin, proto) {
        set.call(origin, proto);
        return origin;
      };
    } catch (e) {
      // do one or more feature detections
      set = {__proto__: null};
      // if proto does not work, needs to fallback
      // some Opera, Rhino, ducktape
      if (set instanceof Object) {
        setPrototypeOf = createAndCopy;
      } else {
        // verify if null objects are buggy
        set.__proto__ = objProto;
        // if null objects are buggy
        // nodejs 0.8 to 0.10
        if (set instanceof Object) {
          setPrototypeOf = function (origin, proto) {
            // use such bug to promote
            origin.__proto__ = proto;
            return origin;
          };
        } else {
          // try to use proto or fallback
          // Safari, old Firefox, many others
          setPrototypeOf = function (origin, proto) {
            // if proto is not null
            return getPrototypeOf(origin) ?
              // use __proto__ to promote
              ((origin.__proto__ = proto), origin) :
              // otherwise unable to promote: fallback
              createAndCopy(origin, proto);
          };
        }
      }
    }
    Object.setPrototypeOf = setPrototypeOf;
  }());

}));
;
(function() {
    "use strict";

    function $$utils$$objectOrFunction(x) {
      return typeof x === 'function' || (typeof x === 'object' && x !== null);
    }

    function $$utils$$isFunction(x) {
      return typeof x === 'function';
    }

    function $$utils$$isMaybeThenable(x) {
      return typeof x === 'object' && x !== null;
    }

    var $$utils$$_isArray;

    if (!Array.isArray) {
      $$utils$$_isArray = function (x) {
        return Object.prototype.toString.call(x) === '[object Array]';
      };
    } else {
      $$utils$$_isArray = Array.isArray;
    }

    var $$utils$$isArray = $$utils$$_isArray;
    var $$utils$$now = Date.now || function() { return new Date().getTime(); };
    function $$utils$$F() { }

    var $$utils$$o_create = (Object.create || function (o) {
      if (arguments.length > 1) {
        throw new Error('Second argument not supported');
      }
      if (typeof o !== 'object') {
        throw new TypeError('Argument must be an object');
      }
      $$utils$$F.prototype = o;
      return new $$utils$$F();
    });

    var $$asap$$len = 0;

    var $$asap$$default = function asap(callback, arg) {
      $$asap$$queue[$$asap$$len] = callback;
      $$asap$$queue[$$asap$$len + 1] = arg;
      $$asap$$len += 2;
      if ($$asap$$len === 2) {
        // If len is 1, that means that we need to schedule an async flush.
        // If additional callbacks are queued before the queue is flushed, they
        // will be processed by this flush that we are scheduling.
        $$asap$$scheduleFlush();
      }
    };

    var $$asap$$browserGlobal = (typeof window !== 'undefined') ? window : {};
    var $$asap$$BrowserMutationObserver = $$asap$$browserGlobal.MutationObserver || $$asap$$browserGlobal.WebKitMutationObserver;

    // test for web worker but not in IE10
    var $$asap$$isWorker = typeof Uint8ClampedArray !== 'undefined' &&
      typeof importScripts !== 'undefined' &&
      typeof MessageChannel !== 'undefined';

    // node
    function $$asap$$useNextTick() {
      return function() {
        process.nextTick($$asap$$flush);
      };
    }

    function $$asap$$useMutationObserver() {
      var iterations = 0;
      var observer = new $$asap$$BrowserMutationObserver($$asap$$flush);
      var node = document.createTextNode('');
      observer.observe(node, { characterData: true });

      return function() {
        node.data = (iterations = ++iterations % 2);
      };
    }

    // web worker
    function $$asap$$useMessageChannel() {
      var channel = new MessageChannel();
      channel.port1.onmessage = $$asap$$flush;
      return function () {
        channel.port2.postMessage(0);
      };
    }

    function $$asap$$useSetTimeout() {
      return function() {
        setTimeout($$asap$$flush, 1);
      };
    }

    var $$asap$$queue = new Array(1000);

    function $$asap$$flush() {
      for (var i = 0; i < $$asap$$len; i+=2) {
        var callback = $$asap$$queue[i];
        var arg = $$asap$$queue[i+1];

        callback(arg);

        $$asap$$queue[i] = undefined;
        $$asap$$queue[i+1] = undefined;
      }

      $$asap$$len = 0;
    }

    var $$asap$$scheduleFlush;

    // Decide what async method to use to triggering processing of queued callbacks:
    if (typeof process !== 'undefined' && {}.toString.call(process) === '[object process]') {
      $$asap$$scheduleFlush = $$asap$$useNextTick();
    } else if ($$asap$$BrowserMutationObserver) {
      $$asap$$scheduleFlush = $$asap$$useMutationObserver();
    } else if ($$asap$$isWorker) {
      $$asap$$scheduleFlush = $$asap$$useMessageChannel();
    } else {
      $$asap$$scheduleFlush = $$asap$$useSetTimeout();
    }

    function $$$internal$$noop() {}
    var $$$internal$$PENDING   = void 0;
    var $$$internal$$FULFILLED = 1;
    var $$$internal$$REJECTED  = 2;
    var $$$internal$$GET_THEN_ERROR = new $$$internal$$ErrorObject();

    function $$$internal$$selfFullfillment() {
      return new TypeError("You cannot resolve a promise with itself");
    }

    function $$$internal$$cannotReturnOwn() {
      return new TypeError('A promises callback cannot return that same promise.')
    }

    function $$$internal$$getThen(promise) {
      try {
        return promise.then;
      } catch(error) {
        $$$internal$$GET_THEN_ERROR.error = error;
        return $$$internal$$GET_THEN_ERROR;
      }
    }

    function $$$internal$$tryThen(then, value, fulfillmentHandler, rejectionHandler) {
      try {
        then.call(value, fulfillmentHandler, rejectionHandler);
      } catch(e) {
        return e;
      }
    }

    function $$$internal$$handleForeignThenable(promise, thenable, then) {
       $$asap$$default(function(promise) {
        var sealed = false;
        var error = $$$internal$$tryThen(then, thenable, function(value) {
          if (sealed) { return; }
          sealed = true;
          if (thenable !== value) {
            $$$internal$$resolve(promise, value);
          } else {
            $$$internal$$fulfill(promise, value);
          }
        }, function(reason) {
          if (sealed) { return; }
          sealed = true;

          $$$internal$$reject(promise, reason);
        }, 'Settle: ' + (promise._label || ' unknown promise'));

        if (!sealed && error) {
          sealed = true;
          $$$internal$$reject(promise, error);
        }
      }, promise);
    }

    function $$$internal$$handleOwnThenable(promise, thenable) {
      if (thenable._state === $$$internal$$FULFILLED) {
        $$$internal$$fulfill(promise, thenable._result);
      } else if (promise._state === $$$internal$$REJECTED) {
        $$$internal$$reject(promise, thenable._result);
      } else {
        $$$internal$$subscribe(thenable, undefined, function(value) {
          $$$internal$$resolve(promise, value);
        }, function(reason) {
          $$$internal$$reject(promise, reason);
        });
      }
    }

    function $$$internal$$handleMaybeThenable(promise, maybeThenable) {
      if (maybeThenable.constructor === promise.constructor) {
        $$$internal$$handleOwnThenable(promise, maybeThenable);
      } else {
        var then = $$$internal$$getThen(maybeThenable);

        if (then === $$$internal$$GET_THEN_ERROR) {
          $$$internal$$reject(promise, $$$internal$$GET_THEN_ERROR.error);
        } else if (then === undefined) {
          $$$internal$$fulfill(promise, maybeThenable);
        } else if ($$utils$$isFunction(then)) {
          $$$internal$$handleForeignThenable(promise, maybeThenable, then);
        } else {
          $$$internal$$fulfill(promise, maybeThenable);
        }
      }
    }

    function $$$internal$$resolve(promise, value) {
      if (promise === value) {
        $$$internal$$reject(promise, $$$internal$$selfFullfillment());
      } else if ($$utils$$objectOrFunction(value)) {
        $$$internal$$handleMaybeThenable(promise, value);
      } else {
        $$$internal$$fulfill(promise, value);
      }
    }

    function $$$internal$$publishRejection(promise) {
      if (promise._onerror) {
        promise._onerror(promise._result);
      }

      $$$internal$$publish(promise);
    }

    function $$$internal$$fulfill(promise, value) {
      if (promise._state !== $$$internal$$PENDING) { return; }

      promise._result = value;
      promise._state = $$$internal$$FULFILLED;

      if (promise._subscribers.length === 0) {
      } else {
        $$asap$$default($$$internal$$publish, promise);
      }
    }

    function $$$internal$$reject(promise, reason) {
      if (promise._state !== $$$internal$$PENDING) { return; }
      promise._state = $$$internal$$REJECTED;
      promise._result = reason;

      $$asap$$default($$$internal$$publishRejection, promise);
    }

    function $$$internal$$subscribe(parent, child, onFulfillment, onRejection) {
      var subscribers = parent._subscribers;
      var length = subscribers.length;

      parent._onerror = null;

      subscribers[length] = child;
      subscribers[length + $$$internal$$FULFILLED] = onFulfillment;
      subscribers[length + $$$internal$$REJECTED]  = onRejection;

      if (length === 0 && parent._state) {
        $$asap$$default($$$internal$$publish, parent);
      }
    }

    function $$$internal$$publish(promise) {
      var subscribers = promise._subscribers;
      var settled = promise._state;

      if (subscribers.length === 0) { return; }

      var child, callback, detail = promise._result;

      for (var i = 0; i < subscribers.length; i += 3) {
        child = subscribers[i];
        callback = subscribers[i + settled];

        if (child) {
          $$$internal$$invokeCallback(settled, child, callback, detail);
        } else {
          callback(detail);
        }
      }

      promise._subscribers.length = 0;
    }

    function $$$internal$$ErrorObject() {
      this.error = null;
    }

    var $$$internal$$TRY_CATCH_ERROR = new $$$internal$$ErrorObject();

    function $$$internal$$tryCatch(callback, detail) {
      try {
        return callback(detail);
      } catch(e) {
        $$$internal$$TRY_CATCH_ERROR.error = e;
        return $$$internal$$TRY_CATCH_ERROR;
      }
    }

    function $$$internal$$invokeCallback(settled, promise, callback, detail) {
      var hasCallback = $$utils$$isFunction(callback),
          value, error, succeeded, failed;

      if (hasCallback) {
        value = $$$internal$$tryCatch(callback, detail);

        if (value === $$$internal$$TRY_CATCH_ERROR) {
          failed = true;
          error = value.error;
          value = null;
        } else {
          succeeded = true;
        }

        if (promise === value) {
          $$$internal$$reject(promise, $$$internal$$cannotReturnOwn());
          return;
        }

      } else {
        value = detail;
        succeeded = true;
      }

      if (promise._state !== $$$internal$$PENDING) {
        // noop
      } else if (hasCallback && succeeded) {
        $$$internal$$resolve(promise, value);
      } else if (failed) {
        $$$internal$$reject(promise, error);
      } else if (settled === $$$internal$$FULFILLED) {
        $$$internal$$fulfill(promise, value);
      } else if (settled === $$$internal$$REJECTED) {
        $$$internal$$reject(promise, value);
      }
    }

    function $$$internal$$initializePromise(promise, resolver) {
      try {
        resolver(function resolvePromise(value){
          $$$internal$$resolve(promise, value);
        }, function rejectPromise(reason) {
          $$$internal$$reject(promise, reason);
        });
      } catch(e) {
        $$$internal$$reject(promise, e);
      }
    }

    function $$$enumerator$$makeSettledResult(state, position, value) {
      if (state === $$$internal$$FULFILLED) {
        return {
          state: 'fulfilled',
          value: value
        };
      } else {
        return {
          state: 'rejected',
          reason: value
        };
      }
    }

    function $$$enumerator$$Enumerator(Constructor, input, abortOnReject, label) {
      this._instanceConstructor = Constructor;
      this.promise = new Constructor($$$internal$$noop, label);
      this._abortOnReject = abortOnReject;

      if (this._validateInput(input)) {
        this._input     = input;
        this.length     = input.length;
        this._remaining = input.length;

        this._init();

        if (this.length === 0) {
          $$$internal$$fulfill(this.promise, this._result);
        } else {
          this.length = this.length || 0;
          this._enumerate();
          if (this._remaining === 0) {
            $$$internal$$fulfill(this.promise, this._result);
          }
        }
      } else {
        $$$internal$$reject(this.promise, this._validationError());
      }
    }

    $$$enumerator$$Enumerator.prototype._validateInput = function(input) {
      return $$utils$$isArray(input);
    };

    $$$enumerator$$Enumerator.prototype._validationError = function() {
      return new Error('Array Methods must be provided an Array');
    };

    $$$enumerator$$Enumerator.prototype._init = function() {
      this._result = new Array(this.length);
    };

    var $$$enumerator$$default = $$$enumerator$$Enumerator;

    $$$enumerator$$Enumerator.prototype._enumerate = function() {
      var length  = this.length;
      var promise = this.promise;
      var input   = this._input;

      for (var i = 0; promise._state === $$$internal$$PENDING && i < length; i++) {
        this._eachEntry(input[i], i);
      }
    };

    $$$enumerator$$Enumerator.prototype._eachEntry = function(entry, i) {
      var c = this._instanceConstructor;
      if ($$utils$$isMaybeThenable(entry)) {
        if (entry.constructor === c && entry._state !== $$$internal$$PENDING) {
          entry._onerror = null;
          this._settledAt(entry._state, i, entry._result);
        } else {
          this._willSettleAt(c.resolve(entry), i);
        }
      } else {
        this._remaining--;
        this._result[i] = this._makeResult($$$internal$$FULFILLED, i, entry);
      }
    };

    $$$enumerator$$Enumerator.prototype._settledAt = function(state, i, value) {
      var promise = this.promise;

      if (promise._state === $$$internal$$PENDING) {
        this._remaining--;

        if (this._abortOnReject && state === $$$internal$$REJECTED) {
          $$$internal$$reject(promise, value);
        } else {
          this._result[i] = this._makeResult(state, i, value);
        }
      }

      if (this._remaining === 0) {
        $$$internal$$fulfill(promise, this._result);
      }
    };

    $$$enumerator$$Enumerator.prototype._makeResult = function(state, i, value) {
      return value;
    };

    $$$enumerator$$Enumerator.prototype._willSettleAt = function(promise, i) {
      var enumerator = this;

      $$$internal$$subscribe(promise, undefined, function(value) {
        enumerator._settledAt($$$internal$$FULFILLED, i, value);
      }, function(reason) {
        enumerator._settledAt($$$internal$$REJECTED, i, reason);
      });
    };

    var $$promise$all$$default = function all(entries, label) {
      return new $$$enumerator$$default(this, entries, true /* abort on reject */, label).promise;
    };

    var $$promise$race$$default = function race(entries, label) {
      /*jshint validthis:true */
      var Constructor = this;

      var promise = new Constructor($$$internal$$noop, label);

      if (!$$utils$$isArray(entries)) {
        $$$internal$$reject(promise, new TypeError('You must pass an array to race.'));
        return promise;
      }

      var length = entries.length;

      function onFulfillment(value) {
        $$$internal$$resolve(promise, value);
      }

      function onRejection(reason) {
        $$$internal$$reject(promise, reason);
      }

      for (var i = 0; promise._state === $$$internal$$PENDING && i < length; i++) {
        $$$internal$$subscribe(Constructor.resolve(entries[i]), undefined, onFulfillment, onRejection);
      }

      return promise;
    };

    var $$promise$resolve$$default = function resolve(object, label) {
      /*jshint validthis:true */
      var Constructor = this;

      if (object && typeof object === 'object' && object.constructor === Constructor) {
        return object;
      }

      var promise = new Constructor($$$internal$$noop, label);
      $$$internal$$resolve(promise, object);
      return promise;
    };

    var $$promise$reject$$default = function reject(reason, label) {
      /*jshint validthis:true */
      var Constructor = this;
      var promise = new Constructor($$$internal$$noop, label);
      $$$internal$$reject(promise, reason);
      return promise;
    };

    var $$es6$promise$promise$$counter = 0;

    function $$es6$promise$promise$$needsResolver() {
      throw new TypeError('You must pass a resolver function as the first argument to the promise constructor');
    }

    function $$es6$promise$promise$$needsNew() {
      throw new TypeError("Failed to construct 'Promise': Please use the 'new' operator, this object constructor cannot be called as a function.");
    }

    var $$es6$promise$promise$$default = $$es6$promise$promise$$Promise;

    /**
      Promise objects represent the eventual result of an asynchronous operation. The
      primary way of interacting with a promise is through its `then` method, which
      registers callbacks to receive either a promise’s eventual value or the reason
      why the promise cannot be fulfilled.

      Terminology
      -----------

      - `promise` is an object or function with a `then` method whose behavior conforms to this specification.
      - `thenable` is an object or function that defines a `then` method.
      - `value` is any legal JavaScript value (including undefined, a thenable, or a promise).
      - `exception` is a value that is thrown using the throw statement.
      - `reason` is a value that indicates why a promise was rejected.
      - `settled` the final resting state of a promise, fulfilled or rejected.

      A promise can be in one of three states: pending, fulfilled, or rejected.

      Promises that are fulfilled have a fulfillment value and are in the fulfilled
      state.  Promises that are rejected have a rejection reason and are in the
      rejected state.  A fulfillment value is never a thenable.

      Promises can also be said to *resolve* a value.  If this value is also a
      promise, then the original promise's settled state will match the value's
      settled state.  So a promise that *resolves* a promise that rejects will
      itself reject, and a promise that *resolves* a promise that fulfills will
      itself fulfill.


      Basic Usage:
      ------------

      ```js
      var promise = new Promise(function(resolve, reject) {
        // on success
        resolve(value);

        // on failure
        reject(reason);
      });

      promise.then(function(value) {
        // on fulfillment
      }, function(reason) {
        // on rejection
      });
      ```

      Advanced Usage:
      ---------------

      Promises shine when abstracting away asynchronous interactions such as
      `XMLHttpRequest`s.

      ```js
      function getJSON(url) {
        return new Promise(function(resolve, reject){
          var xhr = new XMLHttpRequest();

          xhr.open('GET', url);
          xhr.onreadystatechange = handler;
          xhr.responseType = 'json';
          xhr.setRequestHeader('Accept', 'application/json');
          xhr.send();

          function handler() {
            if (this.readyState === this.DONE) {
              if (this.status === 200) {
                resolve(this.response);
              } else {
                reject(new Error('getJSON: `' + url + '` failed with status: [' + this.status + ']'));
              }
            }
          };
        });
      }

      getJSON('/posts.json').then(function(json) {
        // on fulfillment
      }, function(reason) {
        // on rejection
      });
      ```

      Unlike callbacks, promises are great composable primitives.

      ```js
      Promise.all([
        getJSON('/posts'),
        getJSON('/comments')
      ]).then(function(values){
        values[0] // => postsJSON
        values[1] // => commentsJSON

        return values;
      });
      ```

      @class Promise
      @param {function} resolver
      @param {String} label optional string for labeling the promise.
      Useful for tooling.
      @constructor
    */
    function $$es6$promise$promise$$Promise(resolver, label) {
      this._id = $$es6$promise$promise$$counter++;
      this._label = label;
      this._state = undefined;
      this._result = undefined;
      this._subscribers = [];

      if ($$$internal$$noop !== resolver) {
        if (!$$utils$$isFunction(resolver)) {
          $$es6$promise$promise$$needsResolver();
        }

        if (!(this instanceof $$es6$promise$promise$$Promise)) {
          $$es6$promise$promise$$needsNew();
        }

        $$$internal$$initializePromise(this, resolver);
      }
    }

    $$es6$promise$promise$$Promise.all = $$promise$all$$default;
    $$es6$promise$promise$$Promise.race = $$promise$race$$default;
    $$es6$promise$promise$$Promise.resolve = $$promise$resolve$$default;
    $$es6$promise$promise$$Promise.reject = $$promise$reject$$default;

    $$es6$promise$promise$$Promise.prototype = {
      constructor: $$es6$promise$promise$$Promise,

    /**
      The primary way of interacting with a promise is through its `then` method,
      which registers callbacks to receive either a promise's eventual value or the
      reason why the promise cannot be fulfilled.

      ```js
      findUser().then(function(user){
        // user is available
      }, function(reason){
        // user is unavailable, and you are given the reason why
      });
      ```

      Chaining
      --------

      The return value of `then` is itself a promise.  This second, 'downstream'
      promise is resolved with the return value of the first promise's fulfillment
      or rejection handler, or rejected if the handler throws an exception.

      ```js
      findUser().then(function (user) {
        return user.name;
      }, function (reason) {
        return 'default name';
      }).then(function (userName) {
        // If `findUser` fulfilled, `userName` will be the user's name, otherwise it
        // will be `'default name'`
      });

      findUser().then(function (user) {
        throw new Error('Found user, but still unhappy');
      }, function (reason) {
        throw new Error('`findUser` rejected and we're unhappy');
      }).then(function (value) {
        // never reached
      }, function (reason) {
        // if `findUser` fulfilled, `reason` will be 'Found user, but still unhappy'.
        // If `findUser` rejected, `reason` will be '`findUser` rejected and we're unhappy'.
      });
      ```
      If the downstream promise does not specify a rejection handler, rejection reasons will be propagated further downstream.

      ```js
      findUser().then(function (user) {
        throw new PedagogicalException('Upstream error');
      }).then(function (value) {
        // never reached
      }).then(function (value) {
        // never reached
      }, function (reason) {
        // The `PedgagocialException` is propagated all the way down to here
      });
      ```

      Assimilation
      ------------

      Sometimes the value you want to propagate to a downstream promise can only be
      retrieved asynchronously. This can be achieved by returning a promise in the
      fulfillment or rejection handler. The downstream promise will then be pending
      until the returned promise is settled. This is called *assimilation*.

      ```js
      findUser().then(function (user) {
        return findCommentsByAuthor(user);
      }).then(function (comments) {
        // The user's comments are now available
      });
      ```

      If the assimliated promise rejects, then the downstream promise will also reject.

      ```js
      findUser().then(function (user) {
        return findCommentsByAuthor(user);
      }).then(function (comments) {
        // If `findCommentsByAuthor` fulfills, we'll have the value here
      }, function (reason) {
        // If `findCommentsByAuthor` rejects, we'll have the reason here
      });
      ```

      Simple Example
      --------------

      Synchronous Example

      ```javascript
      var result;

      try {
        result = findResult();
        // success
      } catch(reason) {
        // failure
      }
      ```

      Errback Example

      ```js
      findResult(function(result, err){
        if (err) {
          // failure
        } else {
          // success
        }
      });
      ```

      Promise Example;

      ```javascript
      findResult().then(function(result){
        // success
      }, function(reason){
        // failure
      });
      ```

      Advanced Example
      --------------

      Synchronous Example

      ```javascript
      var author, books;

      try {
        author = findAuthor();
        books  = findBooksByAuthor(author);
        // success
      } catch(reason) {
        // failure
      }
      ```

      Errback Example

      ```js

      function foundBooks(books) {

      }

      function failure(reason) {

      }

      findAuthor(function(author, err){
        if (err) {
          failure(err);
          // failure
        } else {
          try {
            findBoooksByAuthor(author, function(books, err) {
              if (err) {
                failure(err);
              } else {
                try {
                  foundBooks(books);
                } catch(reason) {
                  failure(reason);
                }
              }
            });
          } catch(error) {
            failure(err);
          }
          // success
        }
      });
      ```

      Promise Example;

      ```javascript
      findAuthor().
        then(findBooksByAuthor).
        then(function(books){
          // found books
      }).catch(function(reason){
        // something went wrong
      });
      ```

      @method then
      @param {Function} onFulfilled
      @param {Function} onRejected
      @param {String} label optional string for labeling the promise.
      Useful for tooling.
      @return {Promise}
    */
      then: function(onFulfillment, onRejection, label) {
        var parent = this;
        var state = parent._state;

        if (state === $$$internal$$FULFILLED && !onFulfillment || state === $$$internal$$REJECTED && !onRejection) {
          return this;
        }

        parent._onerror = null;

        var child = new this.constructor($$$internal$$noop, label);
        var result = parent._result;

        if (state) {
          var callback = arguments[state - 1];
          $$asap$$default(function(){
            $$$internal$$invokeCallback(state, child, callback, result);
          });
        } else {
          $$$internal$$subscribe(parent, child, onFulfillment, onRejection);
        }

        return child;
      },

    /**
      `catch` is simply sugar for `then(undefined, onRejection)` which makes it the same
      as the catch block of a try/catch statement.

      ```js
      function findAuthor(){
        throw new Error('couldn't find that author');
      }

      // synchronous
      try {
        findAuthor();
      } catch(reason) {
        // something went wrong
      }

      // async with promises
      findAuthor().catch(function(reason){
        // something went wrong
      });
      ```

      @method catch
      @param {Function} onRejection
      @param {String} label optional string for labeling the promise.
      Useful for tooling.
      @return {Promise}
    */
      'catch': function(onRejection, label) {
        return this.then(null, onRejection, label);
      }
    };

    var $$es6$promise$polyfill$$default = function polyfill() {
      var local;

      if (typeof global !== 'undefined') {
        local = global;
      } else if (typeof window !== 'undefined' && window.document) {
        local = window;
      } else {
        local = self;
      }

      var es6PromiseSupport =
        "Promise" in local &&
        // Some of these methods are missing from
        // Firefox/Chrome experimental implementations
        "resolve" in local.Promise &&
        "reject" in local.Promise &&
        "all" in local.Promise &&
        "race" in local.Promise &&
        // Older version of the spec had a resolver object
        // as the arg rather than a function
        (function() {
          var resolve;
          new local.Promise(function(r) { resolve = r; });
          return $$utils$$isFunction(resolve);
        }());

      if (!es6PromiseSupport) {
        local.Promise = $$es6$promise$promise$$default;
      }
    };

    var es6$promise$umd$$ES6Promise = {
      Promise: $$es6$promise$promise$$default,
      polyfill: $$es6$promise$polyfill$$default
    };

    /* global define:true module:true window: true */
    if (typeof define === 'function' && define['amd']) {
      define(function() { return es6$promise$umd$$ES6Promise; });
    } else if (typeof module !== 'undefined' && module['exports']) {
      module['exports'] = es6$promise$umd$$ES6Promise;
    } else if (typeof this !== 'undefined') {
      this['ES6Promise'] = es6$promise$umd$$ES6Promise;
    }
	if ( this['ES6Promise'] && !this.Promise ){
		this.Promise = es6$promise$umd$$ES6Promise.Promise;
		if ( typeof window === 'undefined' ){
			Promise = this.Promise;
		}
	}
}).call(this);;
// JavaScript Document
(function(){
	if ( !this.series ) this.series = new Object();
	
	var global = this.series;
	// Save bytes in the minified (but not gzipped) version:
  var ArrayProto = Array.prototype, ObjProto = Object.prototype, FuncProto = Function.prototype;

  // Create quick reference variables for speed access to core prototypes.
  var
    push             = ArrayProto.push,
    slice            = ArrayProto.slice,
    toString         = ObjProto.toString,
    hasOwnProperty   = ObjProto.hasOwnProperty;

  // All **ECMAScript 5** native function implementations that we hope to use
  // are declared here.
  var
    nativeIsArray      = Array.isArray,
    nativeKeys         = Object.keys,
    nativeBind         = FuncProto.bind,
    nativeCreate       = Object.create;
	
	// Shortcut function for checking if an object has a given property directly
  // on itself (in other words, not on a prototype).
  global.has = function(obj, key) {
    return obj != null && hasOwnProperty.call(obj, key);
  };

  // Is a given value a DOM element?
  global.isElement = function(obj) {
    return !!(obj && obj.nodeType === 1);
  };

  // Is a given value an array?
  // Delegates to ECMA5's native Array.isArray
  global.isArray = nativeIsArray || function(obj) {
    return toString.call(obj) === '[object Array]';
  };

  // Is a given variable an object?
  global.isObject = function(obj) {
    var type = typeof obj;
    return type === 'function' || type === 'object' && !!obj;
  };

  // Add some isType methods: isArguments, isFunction, isString, isNumber, isDate, isRegExp, isError.
	['Arguments', 'Function', 'String', 'Number', 'Date', 'RegExp', 'Error'].forEach(function(name){
		global['is' + name] = function(obj) {
      return toString.call(obj) === '[object ' + name + ']';
    };
	});

  // Define a fallback version of the method in browsers (ahem, IE < 9), where
  // there isn't any inspectable "Arguments" type.
  if (!global.isArguments(arguments)) {
    global.isArguments = function(obj) {
      return global.has(obj, 'callee');
    };
  }

  // Optimize `isFunction` if appropriate. Work around some typeof bugs in old v8,
  // IE 11 (#1621), and in Safari 8 (#1929).
  if (typeof /./ != 'function' && typeof Int8Array != 'object') {
    global.isFunction = function(obj) {
      return typeof obj == 'function' || false;
    };
  }

  // Is a given object a finite number?
  global.isFinite = function(obj) {
    return isFinite(obj) && !isNaN(parseFloat(obj));
  };

  // Is the given value `NaN`? (NaN is the only number which does not equal itself).
  global.isNaN = function(obj) {
    return global.isNumber(obj) && obj !== +obj;
  };

  // Is a given value a boolean?
  global.isBoolean = function(obj) {
    return obj === true || obj === false || toString.call(obj) === '[object Boolean]';
  };

  // Is a given value equal to null?
  global.isNull = function(obj) {
    return obj === null;
  };

  // Is a given variable undefined?
  global.isUndefined = function(obj) {
    return obj === void 0;
  };
}).call(this);;
;(function () {
    'use strict';
		
		if ( typeof JSON === 'undefined' ){
			window.JSON = new Object();
		}
		
		if ( typeof JSON !== 'undefined' && JSON.stringify ){
			return;
		};
		

    function f(n) {
        // Format integers to have at least two digits.
        return n < 10 ? '0' + n : n;
    }

    if (typeof Date.prototype.toJSON !== 'function') {

        Date.prototype.toJSON = function (key) {

            return isFinite(this.valueOf())
                ? this.getUTCFullYear()     + '-' +
                    f(this.getUTCMonth() + 1) + '-' +
                    f(this.getUTCDate())      + 'T' +
                    f(this.getUTCHours())     + ':' +
                    f(this.getUTCMinutes())   + ':' +
                    f(this.getUTCSeconds())   + 'Z'
                : null;
        };

        String.prototype.toJSON      =
            Number.prototype.toJSON  =
            Boolean.prototype.toJSON = function (key) {
                return this.valueOf();
            };
    }

    var cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,
        escapable = /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,
        gap,
        indent,
        meta = {    // table of character substitutions
            '\b': '\\b',
            '\t': '\\t',
            '\n': '\\n',
            '\f': '\\f',
            '\r': '\\r',
            '"' : '\\"',
            '\\': '\\\\'
        },
        rep;


    function quote(string) {

// If the string contains no control characters, no quote characters, and no
// backslash characters, then we can safely slap some quotes around it.
// Otherwise we must also replace the offending characters with safe escape
// sequences.

        escapable.lastIndex = 0;
        return escapable.test(string) ? '"' + string.replace(escapable, function (a) {
            var c = meta[a];
            return typeof c === 'string'
                ? c
                : '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
        }) + '"' : '"' + string + '"';
    }


    function str(key, holder) {

// Produce a string from holder[key].

        var i,          // The loop counter.
            k,          // The member key.
            v,          // The member value.
            length,
            mind = gap,
            partial,
            value = holder[key];

// If the value has a toJSON method, call it to obtain a replacement value.

        if (value && typeof value === 'object' &&
                typeof value.toJSON === 'function') {
            value = value.toJSON(key);
        }

// If we were called with a replacer function, then call the replacer to
// obtain a replacement value.

        if (typeof rep === 'function') {
            value = rep.call(holder, key, value);
        }

// What happens next depends on the value's type.

        switch (typeof value) {
        case 'string':
            return quote(value);

        case 'number':

// JSON numbers must be finite. Encode non-finite numbers as null.

            return isFinite(value) ? String(value) : 'null';

        case 'boolean':
        case 'null':

// If the value is a boolean or null, convert it to a string. Note:
// typeof null does not produce 'null'. The case is included here in
// the remote chance that this gets fixed someday.

            return String(value);

// If the type is 'object', we might be dealing with an object or an array or
// null.

        case 'object':

// Due to a specification blunder in ECMAScript, typeof null is 'object',
// so watch out for that case.

            if (!value) {
                return 'null';
            }

// Make an array to hold the partial results of stringifying this object value.

            gap += indent;
            partial = [];

// Is the value an array?

            if (Object.prototype.toString.apply(value) === '[object Array]') {

// The value is an array. Stringify every element. Use null as a placeholder
// for non-JSON values.

                length = value.length;
                for (i = 0; i < length; i += 1) {
                    partial[i] = str(i, value) || 'null';
                }

// Join all of the elements together, separated with commas, and wrap them in
// brackets.

                v = partial.length === 0
                    ? '[]'
                    : gap
                    ? '[\n' + gap + partial.join(',\n' + gap) + '\n' + mind + ']'
                    : '[' + partial.join(',') + ']';
                gap = mind;
                return v;
            }

// If the replacer is an array, use it to select the members to be stringified.

            if (rep && typeof rep === 'object') {
                length = rep.length;
                for (i = 0; i < length; i += 1) {
                    if (typeof rep[i] === 'string') {
                        k = rep[i];
                        v = str(k, value);
                        if (v) {
                            partial.push(quote(k) + (gap ? ': ' : ':') + v);
                        }
                    }
                }
            } else {

// Otherwise, iterate through all of the keys in the object.

                for (k in value) {
                    if (Object.prototype.hasOwnProperty.call(value, k)) {
                        v = str(k, value);
                        if (v) {
                            partial.push(quote(k) + (gap ? ': ' : ':') + v);
                        }
                    }
                }
            }

// Join all of the member texts together, separated with commas,
// and wrap them in braces.

            v = partial.length === 0
                ? '{}'
                : gap
                ? '{\n' + gap + partial.join(',\n' + gap) + '\n' + mind + '}'
                : '{' + partial.join(',') + '}';
            gap = mind;
            return v;
        }
    }

// If the JSON object does not yet have a stringify method, give it one.

    if (typeof JSON.stringify !== 'function') {
        JSON.stringify = function (value, replacer, space) {

// The stringify method takes a value and an optional replacer, and an optional
// space parameter, and returns a JSON text. The replacer can be a function
// that can replace values, or an array of strings that will select the keys.
// A default replacer method can be provided. Use of the space parameter can
// produce text that is more easily readable.

            var i;
            gap = '';
            indent = '';

// If the space parameter is a number, make an indent string containing that
// many spaces.

            if (typeof space === 'number') {
                for (i = 0; i < space; i += 1) {
                    indent += ' ';
                }

// If the space parameter is a string, it will be used as the indent string.

            } else if (typeof space === 'string') {
                indent = space;
            }

// If there is a replacer, it must be a function or an array.
// Otherwise, throw an error.

            rep = replacer;
            if (replacer && typeof replacer !== 'function' &&
                    (typeof replacer !== 'object' ||
                    typeof replacer.length !== 'number')) {
                throw new Error('JSON.stringify');
            }

// Make a fake root object containing our value under the key of ''.
// Return the result of stringifying the value.

            return str('', {'': value});
        };
    }


// If the JSON object does not yet have a parse method, give it one.

    if (typeof JSON.parse !== 'function') {
        JSON.parse = function (text, reviver) {

// The parse method takes a text and an optional reviver function, and returns
// a JavaScript value if the text is a valid JSON text.

            var j;

            function walk(holder, key) {

// The walk method is used to recursively walk the resulting structure so
// that modifications can be made.

                var k, v, value = holder[key];
                if (value && typeof value === 'object') {
                    for (k in value) {
                        if (Object.prototype.hasOwnProperty.call(value, k)) {
                            v = walk(value, k);
                            if (v !== undefined) {
                                value[k] = v;
                            } else {
                                delete value[k];
                            }
                        }
                    }
                }
                return reviver.call(holder, key, value);
            }


// Parsing happens in four stages. In the first stage, we replace certain
// Unicode characters with escape sequences. JavaScript handles many characters
// incorrectly, either silently deleting them, or treating them as line endings.

            text = String(text);
            cx.lastIndex = 0;
            if (cx.test(text)) {
                text = text.replace(cx, function (a) {
                    return '\\u' +
                        ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
                });
            }

// In the second stage, we run the text against regular expressions that look
// for non-JSON patterns. We are especially concerned with '()' and 'new'
// because they can cause invocation, and '=' because it can cause mutation.
// But just to be safe, we want to reject all unexpected forms.

// We split the second stage into 4 regexp operations in order to work around
// crippling inefficiencies in IE's and Safari's regexp engines. First we
// replace the JSON backslash pairs with '@' (a non-JSON character). Second, we
// replace all simple value tokens with ']' characters. Third, we delete all
// open brackets that follow a colon or comma or that begin the text. Finally,
// we look to see that the remaining characters are only whitespace or ']' or
// ',' or ':' or '{' or '}'. If that is so, then the text is safe for eval.

            if (/^[\],:{}\s]*$/
                    .test(text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, '@')
                        .replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']')
                        .replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) {

// In the third stage we use the eval function to compile the text into a
// JavaScript structure. The '{' operator is subject to a syntactic ambiguity
// in JavaScript: it can begin a block or an object literal. We wrap the text
// in parens to eliminate the ambiguity.

                j = eval('(' + text + ')');

// In the optional fourth stage, we recursively walk the new structure, passing
// each name/value pair to a reviver function for possible transformation.

                return typeof reviver === 'function'
                    ? walk({'': j}, '')
                    : j;
            }

// If the text is not JSON parseable, then a SyntaxError is thrown.

            throw new SyntaxError('JSON.parse');
        };
    };
	
// If the JSON object does not yet have a format method, give it one.
	if (typeof JSON.format !== 'function') {
		var repeat = function(s, count) {
			return new Array(count + 1).join(s);
		}
		
		/**
			options.cr: 
				true: 使用\r\n(windows风格)作为换行符
				false：使用\n(*nux风格)作为换行符，缺省
			options.tab:
				缩进符，默认为tab
		*/
		JSON.format = function(json, options) {

			json = this.stringify(json);
			
			var i           = 0,
				il          = 0,
				cr			= options && options.cr ? options.cr : false,
				tab         = options && options.tab ? options.tab : '\t',
				output   	= '',
				indentLevel = 0,
				inString    = false,
				currentChar = null;
	
			for (i = 0, il = json.length; i < il; i += 1) { 
				currentChar = json.charAt(i);
	
				switch (currentChar) {
				case '{': 
				case '[': 
					if (!inString) { 
						output += currentChar + '\n' + repeat(tab, indentLevel + 1);
						indentLevel += 1; 
					} else { 
						output += currentChar; 
					}
					break; 
				case '}': 
				case ']': 
					if (!inString) { 
						indentLevel -= 1; 
						output += '\n' + repeat(tab, indentLevel) + currentChar; 
					} else { 
						output += currentChar; 
					} 
					break; 
				case ',': 
					if (!inString) { 
						output += ',\n' + repeat(tab, indentLevel); 
					} else { 
						output += currentChar; 
					} 
					break; 
				case ':': 
					if (!inString) { 
						output += ': '; 
					} else { 
						output += currentChar; 
					} 
					break; 
				case ' ':
				case '\n':
				case '\t':
					if (inString) {
						output += currentChar;
					}
					break;
				case '"': 
					if (i > 0 && json.charAt(i - 1) !== '\\') {
						inString = !inString; 
					}
					output += currentChar; 
					break;
				default: 
					output += currentChar; 
					break;                    
				}
			}
	
			if (cr) {
				output = output.replace(/\n/g, '\r\n'); 
			};
			
			return output;
		}
	}
}());;
/**
 * Copyright © kacper.wang 
 * http://jaywcjlove.github.io
 */
;(function(){
	/**
	 * [format 日期格式化]
	 * @param  {[type]} format ["YYYY年MM月dd日hh小时mm分ss秒"]
	 * @return {[type]}        [string]
	 */
	Date.prototype.format = function(format){ 
		var o = { 
			"M+" : this.getMonth()+1, //month 
			"d+" : this.getDate(), //day 
			"h+" : this.getHours(), //hour 
			"m+" : this.getMinutes(), //minute 
			"s+" : this.getSeconds(), //second 
			"q+" : Math.floor((this.getMonth()+3)/3), //quarter 
			"S" : this.getMilliseconds() //millisecond 
		}
		if(/(y+)/.test(format))
			format = format.replace(RegExp.$1, (this.getFullYear()+"").substr(4 - RegExp.$1.length));
		for(var k in o) 
			if(new RegExp("("+ k +")").test(format)) 
			format = format.replace(RegExp.$1, RegExp.$1.length==1 ? o[k] : ("00"+ o[k]).substr((""+ o[k]).length));
		return format; 
	}
	/**
	 * [ago 多少小时前、多少分钟前、多少秒前]
	 * @return {[type]} [string]
	 */
	Date.prototype.ago = function(){ 
		if(!arguments.length) return '';
		var arg = arguments,
			now=this.getTime(),
			past =  !isNaN(arg[0])?arg[0]:new Date(arg[0]).getTime(),
			diffValue = now - past,
			result='',
			minute = 1000 * 60,
			hour = minute * 60,
			day = hour * 24,
			halfamonth = day * 15,
			month = day * 30,
			year = month * 12,

			_year = diffValue/year,
			_month =diffValue/month,
			_week =diffValue/(7*day),
			_day =diffValue/day,
			_hour =diffValue/hour,
			_min =diffValue/minute;

		if(_year>=1) result=parseInt(_year) + "年前";
		else if(_month>=1) result=parseInt(_month) + "个月前";
		else if(_week>=1) result=parseInt(_week) + "周前";
		else if(_day>=1) result=parseInt(_day) +"天前";
		else if(_hour>=1) result=parseInt(_hour) +"个小时前";
		else if(_min>=1) result=parseInt(_min) +"分钟前";
		else result="刚刚";
		return result;
	}
})();;
(function(h){var e={},a=/11(4|5)Browser|2345(Explorer|chrome)|360se|360ee|360 aphone browser|Abolimba|Acoo Browser|ANTFresco|Alienforce|Amaya|Amiga-AWeb|MRCHROME|America Online Browser|AmigaVoyager|AOL|Arora|AtomicBrowser|BarcaPro|Barca|Beamrise|Beonex|BA?IDUBrowser|BaiduHD|Blackbird|BlackHawk|Blazer|Bolt|BonEcho|BrowseX|Browzar|Bunjalloo|Camino|Cayman Browser|Charon|Cheshire|Chimera|chromeframe|ChromePlus|curl|Iron|Chromium|Classilla|Coast|Columbus|CometBird|Comodo_Dragon|Conkeror|CoolNovo|CoRom|Crazy Browser|CrMo|Cruz|Cyberdog|Deepnet Explorer|Demeter|DeskBrowse|Dillo|DoCoMo|DocZilla|Dolfin|Dooble|Doris|Dorothy|DPlus|Edbrowse|E?links|Element Browser|Enigma Browser|EnigmaFox|Epic|Epiphany|Escape|Espial|Fennec|Firebird|Fireweb Navigator|Flock|Fluid|Galeon|GlobalMojo|GoBrowser|Google Wireless Transcoder|GoSurf|GranParadiso|GreenBrowser|Hana|HotJava|Hv3|Hydra Browser|Iris|IBM WebExplorer|JuziBrowser|MiuiBrowser|MxNitro|IBrowse|iCab|IceBrowser|Iceape|IceCat|IceDragon|IceWeasel|iNet Browser|iRider|InternetSurfboard|Jasmine|K-Meleon|K-Ninja|Kapiko|Kazehakase|Strata|KKman|Kinza|KMail|KMLite|Konqueror|Kylo|LBrowser|LBBrowser|Liebaofast|LeechCraft|Lobo|lolifox|Lorentz|Lunascape|Lynx|Madfox|Maemo Browser|Maxthon| MIB\/|Tablet browser|MicroMessenger|Midori|Minefield|MiniBrowser|Minimo|Mosaic|MozillaDeveloperPreview|Multi-Browser|MultiZilla|MyIE2|myibrow|Namoroka|Navigator|NetBox|NetCaptor|NetFront|NetNewsWire|NetPositive|Netscape|NetSurf|NF-Browser|Nichrome\/self|NokiaBrowser|Novarra-Vision|Obigo|OffByOne|OmniWeb|OneBrowser|Orca|Oregano|Origyn Web Browser|osb-browser|Otter| Pre\/|Palemoon|Patriott::Browser|Perk|Phaseout|Phoenix|PlayStation 4|Podkicker|Podkicker Pro|Pogo|Polaris|Polarity|Prism|M?QQBrowser|QQ(?!Download|Pinyin)|QtWeb Internet Browser|QtCarBrowser|QupZilla|rekonq|retawq|RockMelt|Ryouko|SaaYaa|SeaMonkey|SEMC-Browser|SEMC-java|Shiira|Shiretoko|SiteKiosk|SkipStone|Skyfire|Sleipnir|Silk|SlimBoat|SlimBrowser|Superbird|SmartTV|Songbird|Stainless|SubStream|Sulfur|Sundance|Sunrise|Surf|Swiftfox|Swiftweasel|Sylera|TaoBrowser|tear|TeaShark|Teleca|TenFourFox|TheWorld|Thunderbird|Tizen|Tjusig|TencentTraveler|UC? ?Browser|UCWEB|UltraBrowser|UP.Browser|UP.Link|Usejump|uZardWeb|uZard|uzbl|Vimprobable|Vivaldi|Vonkeror|w3m|IEMobile|Waterfox|WebianShell|Webrender|WeltweitimnetzBrowser|wKiosk|WorldWideWeb|wget|WhiteHat Aviator|Wyzo|X-Smiles|Xiino|YaBrowser|zBrowser|ZipZap/i,
b={"114browser":{title:"{%114Browser%}",image:"114browser"},"115browser":{title:"{%115Browser%}",image:"115browser"},"2345explorer":{title:"{%2345Explorer%}",image:"2345explorer"},"2345chrome":{title:"{%2345Chrome%}",image:"2345chrome"},"360se":{title:"360 Explorer",image:"360se"},"360ee":{title:"360 Chrome",image:"360se"},"360 aphone browser":{title:"360 Aphone Browser",image:"360se"},abolimba:{title:"Abolimba",image:"abolimba"},"acoo browser":{title:"Acoo {%Browser%}",image:"acoobrowser"},alienforce:{title:"{%Alienforce%}",
image:"alienforce"},amaya:{title:"{%Amaya%}",image:"amaya"},"amiga-aweb":{title:"Amiga {%AWeb%}",image:"amiga-aweb"},antfresco:{title:"ANT {%Fresco%}",image:"antfresco"},mrchrome:{title:"Amigo",image:"amigo"},myibrow:{title:"{%myibrow%}",image:"my-internet-browser"},"america online browser":{title:"America Online {%Browser%}",image:"aol"},amigavoyager:{title:"Amiga {%Voyager%}",image:"amigavoyager"},aol:{title:"{%AOL%}",image:"aol"},arora:{title:"{%Arora%}",image:"arora"},atomicbrowser:{title:"{%AtomicBrowser%}",
image:"atomicwebbrowser"},barcapro:{title:"{%BarcaPro%}",image:"barca"},barca:{title:"{%Barca%}",image:"barca"},beamrise:{title:"{%Beamrise%}",image:"beamrise"},beonex:{title:"{%Beonex%}",image:"beonex"},baidubrowser:{title:"{%baidubrowser%}",image:"bidubrowser"},bidubrowser:{title:"{%bidubrowser%}",image:"bidubrowser"},baiduhd:{title:"{%BaiduHD%}",image:"bidubrowser"},blackbird:{title:"{%Blackbird%}",image:"blackbird"},blackhawk:{title:"{%BlackHawk%}",image:"blackhawk"},blazer:{title:"{%Blazer%}",
image:"blazer"},bolt:{title:"{%Bolt%}",image:"bolt"},bonecho:{title:"{%BonEcho%}",image:"firefoxdevpre"},browsex:{title:"BrowseX",image:"browsex"},browzar:{title:"{%Browzar%}",image:"browzar"},bunjalloo:{title:"{%Bunjalloo%}",image:"bunjalloo"},camino:{title:"{%Camino%}",image:"camino"},"cayman browser":{title:"Cayman {%Browser%}",image:"caymanbrowser"},charon:{title:"{%Charon%}",image:"null"},cheshire:{title:"{%Cheshire%}",image:"aol"},chimera:{title:"{%Chimera%}",image:"null"},chromeframe:{title:"{%chromeframe%}",
image:"chrome"},chromeplus:{title:"{%ChromePlus%}",image:"chromeplus"},iron:{title:"SRWare {%Iron%}",image:"srwareiron"},chromium:{title:"{%Chromium%}",image:"chromium"},classilla:{title:"{%Classilla%}",image:"classilla"},coast:{title:"{%Coast%}",image:"coast"},columbus:{title:"{%Columbus%}",image:"columbus"},cometbird:{title:"{%CometBird%}",image:"cometbird"},comodo_dragon:{title:"Comodo {%Dragon%}",image:"comodo-dragon"},conkeror:{title:"{%Conkeror%}",image:"conkeror"},coolnovo:{title:"{%CoolNovo%}",
image:"coolnovo"},corom:{title:"{%CoRom%}",image:"corom"},"crazy browser":{title:"Crazy {%Browser%}",image:"crazybrowser"},crmo:{title:"{%CrMo%}",image:"chrome"},cruz:{title:"{%Cruz%}",image:"cruz"},cyberdog:{title:"{%Cyberdog%}",image:"cyberdog"},dplus:{title:"{%DPlus%}",image:"dillo"},"deepnet explorer":{title:"{%Deepnet Explorer%}",image:"deepnetexplorer"},demeter:{title:"{%Demeter%}",image:"demeter"},deskbrowse:{title:"{%DeskBrowse%}",image:"deskbrowse"},dillo:{title:"{%Dillo%}",image:"dillo"},
docomo:{title:"{%DoCoMo%}",image:"null"},doczilla:{title:"{%DocZilla%}",image:"doczilla"},dolfin:{title:"{%Dolfin%}",image:"samsung"},dooble:{title:"{%Dooble%}",image:"dooble"},doris:{title:"{%Doris%}",image:"doris"},dorothy:{title:"{%Dorothy%}",image:"dorothybrowser"},edbrowse:{title:"{%Edbrowse%}",image:"edbrowse"},elinks:{title:"{%Elinks%}",image:"elinks"},"element browser":{title:"Element {%Browser%}",image:"elementbrowser"},"enigma browser":{title:"Enigma {%Browser%}",image:"enigmabrowser"},
enigmafox:{title:"{%EnigmaFox%}",image:"null"},epic:{title:"{%Epic%}",image:"epicbrowser"},epiphany:{title:"{%Epiphany%}",image:"epiphany"},escape:{title:"{%Escape%}",image:"espialtvbrowser"},espial:{title:"{%Espial%}",image:"espialtvbrowser"},fennec:{title:"{%Fennec%}",image:"fennec"},firebird:{title:"{%Firebird%}",image:"firebird"},"fireweb navigator":{title:"{%Fireweb Navigator%}",image:"firewebnavigator"},flock:{title:"{%Flock%}",image:"flock"},fluid:{title:"{%Fluid%}",image:"fluid"},galeon:{title:"{%Galeon%}",
image:"galeon"},globalmojo:{title:"{%GlobalMojo%}",image:"globalmojo"},gobrowser:{title:"GO {%Browser%}",image:"gobrowser"},"google wireless transcoder":{title:"Google Wireless Transcoder",image:"google"},gosurf:{title:"{%GoSurf%}",image:"gosurf"},granparadiso:{title:"{%GranParadiso%}",image:"firefoxdevpre"},greenbrowser:{title:"{%GreenBrowser%}",image:"greenbrowser"},hana:{title:"{%Hana%}",image:"hana"},hotjava:{title:"{%HotJava%}",image:"hotjava"},hv3:{title:"{%Hv3%}",image:"hv3"},"hydra browser":{title:"Hydra Browser",
image:"hydrabrowser"},iris:{title:"{%Iris%}",image:"iris"},"ibm webexplorer":{title:"IBM {%WebExplorer%}",image:"ibmwebexplorer"},juzibrowser:{title:"JuziBrowser",image:"juzibrowser"},miuibrowser:{title:"{%MiuiBrowser%}",image:"miuibrowser"},mxnitro:{title:"{%MxNitro%}",image:"mxnitro"},ibrowse:{title:"{%IBrowse%}",image:"ibrowse"},icab:{title:"{%iCab%}",image:"icab"},icebrowser:{title:"{%IceBrowser%}",image:"icebrowser"},iceape:{title:"{%Iceape%}",image:"iceape"},icecat:{title:"GNU {%IceCat%}",image:"icecat"},
icedragon:{title:"{%IceDragon%}",image:"icedragon"},iceweasel:{title:"{%IceWeasel%}",image:"iceweasel"},"inet browser":{title:"iNet {%Browser%}",image:"null"},irider:{title:"{%iRider%}",image:"irider"},internetsurfboard:{title:"{%InternetSurfboard%}",image:"internetsurfboard"},jasmine:{title:"{%Jasmine%}",image:"samsung"},"k-meleon":{title:"{%K-Meleon%}",image:"kmeleon"},"k-ninja":{title:"{%K-Ninja%}",image:"kninja"},kapiko:{title:"{%Kapiko%}",image:"kapiko"},kazehakase:{title:"{%Kazehakase%}",image:"kazehakase"},
strata:{title:"Kirix {%Strata%}",image:"kirix-strata"},kkman:{title:"{%KKman%}",image:"kkman"},kinza:{title:"{%Kinza%}",image:"kinza"},kmail:{title:"{%KMail%}",image:"kmail"},kmlite:{title:"{%KMLite%}",image:"kmeleon"},konqueror:{title:"{%Konqueror%}",image:"konqueror"},kylo:{title:"{%Kylo%}",image:"kylo"},lbrowser:{title:"{%LBrowser%}",image:"lbrowser"},links:{title:"{%Links%}",image:"null"},lbbrowser:{title:"Liebao Browser",image:"lbbrowser"},liebaofast:{title:"{%Liebaofast%}",image:"lbbrowser"},
leechcraft:{title:"LeechCraft",image:"null"},lobo:{title:"{%Lobo%}",image:"lobo"},lolifox:{title:"{%lolifox%}",image:"lolifox"},lorentz:{title:"{%Lorentz%}",image:"firefoxdevpre"},lunascape:{title:"{%Lunascape%}",image:"lunascape"},lynx:{title:"{%Lynx%}",image:"lynx"},madfox:{title:"{%Madfox%}",image:"madfox"},"maemo browser":{title:"{%Maemo Browser%}",image:"maemo"},maxthon:{title:"{%Maxthon%}",image:"maxthon"}," mib/":{title:"{%MIB%}",image:"mib"},"tablet browser":{title:"{%Tablet browser%}",image:"microb"},
micromessenger:{title:"{%MicroMessenger%}",image:"wechat"},midori:{title:"{%Midori%}",image:"midori"},minefield:{title:"{%Minefield%}",image:"minefield"},minibrowser:{title:"{%MiniBrowser%}",image:"minibrowser"},minimo:{title:"{%Minimo%}",image:"minimo"},mosaic:{title:"{%Mosaic%}",image:"mosaic"},mozilladeveloperpreview:{title:"{%MozillaDeveloperPreview%}",image:"firefoxdevpre"},mqqbrowser:{title:"{%MQQBrowser%}",image:"qqbrowser"},"multi-browser":{title:"{%Multi-Browser%}",image:"multi-browserxp"},
multizilla:{title:"{%MultiZilla%}",image:"mozilla"},myie2:{title:"{%MyIE2%}",image:"myie2"},namoroka:{title:"{%Namoroka%}",image:"firefoxdevpre"},navigator:{title:"Netscape {%Navigator%}",image:"netscape"},netbox:{title:"{%NetBox%}",image:"netbox"},netcaptor:{title:"{%NetCaptor%}",image:"netcaptor"},netfront:{title:"{%NetFront%}",image:"netfront"},netnewswire:{title:"{%NetNewsWire%}",image:"netnewswire"},netpositive:{title:"{%NetPositive%}",image:"netpositive"},netscape:{title:"{%Netscape%}",image:"netscape"},
netsurf:{title:"{%NetSurf%}",image:"netsurf"},"nf-browser":{title:"{%NF-Browser%}",image:"netfront"},"nichrome/self":{title:"{%Nichrome/self%}",image:"nichromeself"},nokiabrowser:{title:"Nokia {%Browser%}",image:"nokia"},"novarra-vision":{title:"Novarra {%Vision%}",image:"novarra"},obigo:{title:"{%Obigo%}",image:"obigo"},offbyone:{title:"Off By One",image:"offbyone"},omniweb:{title:"{%OmniWeb%}",image:"omniweb"},onebrowser:{title:"{%OneBrowser%}",image:"onebrowser"},orca:{title:"{%Orca%}",image:"orca"},
oregano:{title:"{%Oregano%}",image:"oregano"},"origyn web browser":{title:"Oregano Web Browser",image:"owb"},"osb-browser":{title:"{%osb-browser%}",image:"null"},otter:{title:"{%Otter%}",image:"otter"}," pre/":{title:"Palm {%Pre%}",image:"palmpre"},palemoon:{title:"Pale {%Moon%}",image:"palemoon"},"patriott::browser":{title:"Patriott {%Browser%}",image:"patriott"},perk:{title:"{%Perk%}",image:"perk"},phaseout:{title:"Phaseout",image:"phaseout"},phoenix:{title:"{%Phoenix%}",image:"phoenix"},"playstation 4":{title:"PS4 Web Browser",
image:"webkit"},podkicker:{title:"{%Podkicker%}",image:"podkicker"},"podkicker pro":{title:"{%Podkicker Pro%}",image:"podkicker"},pogo:{title:"{%Pogo%}",image:"pogo"},polaris:{title:"{%Polaris%}",image:"polaris"},polarity:{title:"{%Polarity%}",image:"polarity"},prism:{title:"{%Prism%}",image:"prism"},qqbrowser:{title:"{%QQBrowser%}",image:"qqbrowser"},qq:{title:"{%QQ%}",image:"qq"},"qtweb internet browser":{title:"QtWeb Internet {%Browser%}",image:"qtwebinternetbrowser"},qtcarbrowser:{title:"{%qtcarbrowser%}",
image:"tesla"},qupzilla:{title:"{%QupZilla%}",image:"qupzilla"},rekonq:{title:"rekonq",image:"rekonq"},retawq:{title:"{%retawq%}",image:"terminal"},rockmelt:{title:"{%RockMelt%}",image:"rockmelt"},ryouko:{title:"{%Ryouko%}",image:"ryouko"},saayaa:{title:"SaaYaa Explorer",image:"saayaa"},seamonkey:{title:"{%SeaMonkey%}",image:"seamonkey"},"semc-browser":{title:"{%SEMC-Browser%}",image:"semcbrowser"},"semc-java":{title:"{%SEMC-java%}",image:"semcbrowser"},shiira:{title:"{%Shiira%}",image:"shiira"},
shiretoko:{title:"{%Shiretoko%}",image:"firefoxdevpre"},sitekiosk:{title:"{%SiteKiosk%}",image:"sitekiosk"},skipstone:{title:"{%SkipStone%}",image:"skipstone"},skyfire:{title:"{%Skyfire%}",image:"skyfire"},sleipnir:{title:"{%Sleipnir%}",image:"sleipnir"},silk:{title:"Amazon {%Silk%}",image:"silk"},slimboat:{title:"{%SlimBoat%}",image:"slimboat"},slimbrowser:{title:"{%SlimBrowser%}",image:"slimbrowser"},superbird:{title:"{%Superbird%}",image:"superbird"},smarttv:{title:"{%SmartTV%}",image:"maplebrowser"},
songbird:{title:"{%Songbird%}",image:"songbird"},stainless:{title:"{%Stainless%}",image:"stainless"},substream:{title:"{%SubStream%}",image:"substream"},sulfur:{title:"Flock {%Sulfur%}",image:"flock"},sundance:{title:"{%Sundance%}",image:"sundance"},sunrise:{title:"{%Sunrise%}",image:"sunrise"},surf:{title:"{%Surf%}",image:"surf"},swiftfox:{title:"{%Swiftfox%}",image:"swiftfox"},swiftweasel:{title:"{%Swiftweasel%}",image:"swiftweasel"},sylera:{title:"{%Sylera%}",image:"null"},taobrowser:{title:"{%TaoBrowser%}",
image:"taobrowser"},tear:{title:"Tear",image:"tear"},teashark:{title:"{%TeaShark%}",image:"teashark"},teleca:{title:"{%Teleca%}",image:"obigo"},tencenttraveler:{title:"Tencent {%Traveler%}",image:"tencenttraveler"},tenfourfox:{title:"{%TenFourFox%}",image:"tenfourfox"},theworld:{title:"TheWorld Browser",image:"theworld"},thunderbird:{title:"{%Thunderbird%}",image:"thunderbird"},tizen:{title:"{%Tizen%}",image:"tizen"},tjusig:{title:"{%Tjusig%}",image:"tjusig"},ubrowser:{title:"{%UBrowser%}",image:"ucbrowser"},
ucbrowser:{title:"{%UCBrowser%}",image:"ucbrowser"},"uc browser":{title:"{%UC Browser%}",image:"ucbrowser"},ucweb:{title:"{%UCWEB%}",image:"ucbrowser"},ultrabrowser:{title:"{%UltraBrowser%}",image:"ultrabrowser"},"up.browser":{title:"{%UP.Browser%}",image:"openwave"},"up.link":{title:"{%UP.Link%}",image:"openwave"},usejump:{title:"{%Usejump%}",image:"usejump"},uzardweb:{title:"{%uZardWeb%}",image:"uzardweb"},uzard:{title:"{%uZard%}",image:"uzardweb"},uzbl:{title:"uzbl",image:"uzbl"},vimprobable:{title:"{%Vimprobable%}",
image:"null"},vivaldi:{title:"{%Vivaldi%}",image:"vivaldi"},vonkeror:{title:"{%Vonkeror%}",image:"null"},w3m:{title:"{%W3M%}",image:"w3m"},wget:{title:"{%wget%}",image:"null"},curl:{title:"{%curl%}",image:"null"},iemobile:{title:"{%IEMobile%}",image:"msie-mobile"},waterfox:{title:"{%WaterFox%}",image:"waterfox"},webianshell:{title:"Webian {%Shell%}",image:"webianshell"},webrender:{title:"Webrender",image:"webrender"},weltweitimnetzbrowser:{title:"Weltweitimnetz {%Browser%}",image:"weltweitimnetzbrowser"},
"whitehat aviator":{title:"{%WhiteHat Aviator%}",image:"aviator"},wkiosk:{title:"wKiosk",image:"wkiosk"},worldwideweb:{title:"{%WorldWideWeb%}",image:"worldwideweb"},wyzo:{title:"{%Wyzo%}",image:"Wyzo"},"x-smiles":{title:"{%X-Smiles%}",image:"x-smiles"},xiino:{title:"{%Xiino%}",image:"null"},yabrowser:{title:"Yandex.{%Browser%}",image:"yandex"},zbrowser:{title:"{%zBrowser%}",image:"zbrowser"},zipzap:{title:"{%ZipZap%}",image:"zipzap"},abrowse:{title:"ABrowse {%Browser%}",image:"abrowse"},firefox:{title:"{%Firefox%}",
image:"firefox"},none:{title:"Unknown",image:"unknown"}},g=function(a,c){a.image=c.image;a.full=c.title.replace(/\{\%(.+)\%\}/,function(c,b){return d(a,b)})},d=function(a,c){var b=c.toLowerCase();a.name=c;var d=b;"opera"!=b&&"opera next"!=b&&"opera labs"!=b||!/Version/i.test(a.ua)?"opera"!=b&&"opera next"!=b&&"opera developer"!=b||!/OPR/i.test(a.ua)?"opera mobi"==b&&/Version/i.test(a.ua)?d="Version":"safari"==b&&/Version/i.test(a.ua)?d="Version":"pre"==b&&/Version/i.test(a.ua)?d="Version":"android webkit"==
b?d="Version":"links"==b?d="Links (":"uc browser"==b?d="UC Browser":"tenfourfox"==b?d=" rv":"classilla"==b?d=" rv":"smarttv"==b?d="WebBrowser":"ucweb"==b&&/UCBrowser/i.test(a.ua)?d="UCBrowser":"msie"==b&&/\ rv:([.0-9a-zA-Z]+)/i.test(a.ua)?d=" rv":"spartan"==b?d="edge":"nichrome/self"==b&&(d="self"):d="OPR":d="Version";d=d.replace(RegExp("[.\\\\+*?\\[\\^\\]$(){}=!<>|:\\-]","g"),"\\$&");d=a.ua.match(new RegExp(d+"[ |/|:]?([.0-9a-zA-Z]+)","i"));a.version=null!==d?d[1]:"";if("msie"==b&&"7.0"==a.version&&
/Trident\/4.0/i.test(a.ua))return" 8.0 (Compatibility Mode)";if("msie"==b)return" "+a.version;if("nf-browser"==b)return a.name="NetFront","NetFront "+a.version;if("semc-browser"==b)return a.name="SEMC Browser","SEMC Browser "+a.version;if("ucweb"==b||"ubrowser"==b||"ucbrowser"==b||"uc browser"==b)return a.name="UC Browser","UC Browser "+a.version;if("bidubrowser"==b||"baidubrowser"==b||"baiduhd"==b)return a.name="Baidu Browser","Baidu Browser "+a.version;if("up.browser"==b||"up.link"==b)return a.name=
"Openwave Mobile Browser","Openwave Mobile Browser "+a.version;if("chromeframe"==b)return a.name="Google Chrome Frame","Google Chrome Frame "+a.version;if("mozilladeveloperpreview"==b)return a.name="Mozilla Developer Preview","Mozilla Developer Preview "+a.version;if("opera mobi"==b)return a.name="Opera Mobile","Opera Mobile "+a.version;if("osb-browser"==b)return a.name="Gtk+ WebCore","Gtk+ WebCore "+a.version;if("tablet browser"==b)return a.name="MicroB","MicroB "+a.version;if("crmo"==b)return a.name=
"Chrome Mobile","Chrome Mobile "+a.version;if("smarttv"==b)return a.name="Maple Browser","Maple Browser "+a.version;if("atomicbrowser"==b)return a.name="Atomic Web Browser","Atomic Web Browser "+a.version;if("barcapro"==b)return a.name="Barca Pro","Barca Pro "+a.version;if("dplus"==b)return a.name="D+","D+ "+a.version;if("micromessenger"==b)return a.name="WeChat","WeChat "+a.version;if("nichrome/self"==b)return a.name="NiChrome","NiChrome "+a.version;if("opera labs"==b)return d=a.ua.match(/Edition\ Labs([\ ._0-9a-zA-Z]+);/i),
a.version=null!==d?d[1]:"",a.name+" "+a.version;if("qtcarbrowser"==b)return"Tesla Car Browser";"iceweasel"==b&&"Firefox"==a.version&&(a.version="");return a.name+" "+a.version};e.analyze=function(f){var c={ua:f,name:"",version:"",full:"",image:"",dir:"browser"};f=f.match(a);var e=null;null!==f?(f=f[0].toLowerCase(),e="undefined"!==typeof b[f]?b[f]:b.none,g(c,e)):/Galaxy/i.test(c.ua)&&!/Chrome/i.test(c.ua)?(c.full=d(c,"Galaxy"),c.image="galaxy"):/Opera Mini/i.test(c.ua)?(c.full=d(c,"Opera Mini"),c.image=
"opera-2"):/Opera Mobi/i.test(c.ua)?(c.full=d(c,"Opera Mobi"),c.image="opera-2"):/Opera Labs/i.test(c.ua)||/Opera/i.test(c.ua)&&/Edition Labs/i.test(c.ua)?(c.full=d(c,"Opera Labs"),c.image="opera-next"):/Opera Next/i.test(c.ua)||/Opera/i.test(c.ua)&&/Edition Next/i.test(c.ua)?(c.full=d(c,"Opera Next"),c.image="opera-next"):/Opera/i.test(c.ua)?(c.full=d(c,"Opera"),c.image="opera-1",/Version/i.test(c.ua)&&(c.image="opera-2")):/OPR/i.test(c.ua)?/(Edition Next)/i.test(c.ua)?(c.full=d(c,"Opera Next"),
c.image="opera-next"):/(Edition Developer)/i.test(c.ua)?(c.full=d(c,"Opera Developer"),c.image="opera-developer"):(c.full=d(c,"Opera"),c.image="opera-1"):/Series60/i.test(c.ua)&&!/Symbian/i.test(c.ua)?(c.full="Nokia "+d(c,"Series60"),c.image="s60"):/S60/i.test(c.ua)&&!/Symbian/i.test(c.ua)?(c.full="Nokia "+d(c,"S60"),c.image="s60"):/SE\ /i.test(c.ua)&&/MetaSr/i.test(c.ua)?(c.name=c.full="Sogou Explorer",c.image="sogou"):/Ubuntu\;\ Mobile/i.test(c.ua)||/Ubuntu\;\ Tablet/i.test(c.ua)&&/WebKit/i.test(c.ua)?
(c.name=c.full="Ubuntu Web Browser",c.image="ubuntuwebbrowser"):/Avant\ Browser/i.test(c.ua)?(c.full="Avant "+d(c,"Browser"),c.image="avantbrowser"):/AppleWebkit/i.test(c.ua)&&/Android/i.test(c.ua)&&!/Chrome/i.test(c.ua)?(c.full=d(c,"Android Webkit"),c.image="android-webkit"):/Chrome|crios/i.test(c.ua)?/Windows NT 1.+Edge/i.test(c.ua)?(c.full="Internet Explorer "+d(c,"Spartan"),c.image="msie11"):(/crios/i.test(c.ua)?c.full="Google "+d(c,"CriOS"):c.full="Google "+d(c,"Chrome"),c.image="chrome"):/Safari/i.test(c.ua)&&
!/Nokia/i.test(c.ua)?(c.name="Safari",/Version/i.test(c.ua)?c.full=d(c,"Safari"):c.full=c.name,/Mobile ?Safari/i.test(c.ua)&&(c.name="Mobile "+c.name,c.full="Mobile "+c.full),c.image="safari"):/Nokia/i.test(c.ua)&&!/Trident/i.test(c.ua)?(c.full="Nokia Web Browser",c.image="maemo"):/Firefox/i.test(c.ua)?(c.full=d(c,"Firefox"),c.image="firefox"):/MSIE/i.test(c.ua)||/Trident/i.test(c.ua)?(c.full="Internet Explorer"+d(c,"MSIE"),c.image="msie",e=c.ua.match(/(MSIE[\ |\/]?| rv:)([.0-9a-zA-Z]+)/i),null!==
e&&(f=parseInt(e[2]),11<=f?c.image="msie11":10<=f?c.image="msie10":9<=f?c.image="msie9":7<=f?c.image="msie7":6<=f?c.image="msie6":4<=f?c.image="msie4":3<=f?c.image="msie3":2<=f&&(c.image="msie2"))):/Mozilla/i.test(c.ua)?(c.full="Mozilla Compatible",c.image="mozilla"):(c.name="Unknown",c.image="null",c.full=c.name);return c};"undefined"!==typeof module&&module.exports?module.exports=e:"undefined"!==typeof define&&define.amd?define([],function(){return e}):"undefined"!==typeof define&&define.cmd?define([],
function(a,b,d){d.exports=e}):(h.USERAGENT_BROWSER=function(){},USERAGENT_BROWSER.prototype.analyze=e.analyze)})(this);
(function(h){var e={analyze:function(a){a={ua:a,name:"",image:"",dir:"device"};var b=null;if(/(MEIZU (MX|M9)|M030)|MX-3/i.test(a.ua))a.name="Meizu",a.image="meizu";else if(/MI-ONE|MI \d|HM NOTE/i.test(a.ua))a.name="Xiaomi",(b=a.ua.match(/HM NOTE ([A-Z0-9]+)/i))?a.name+=" HM-NOTE "+b[1]:(b=a.ua.match(/MI ([A-Z0-9]+)/i))?a.name+=" "+b[1]:a.ua.match(/MI-ONE/i)&&(a.name+=" 1"),a.image="xiaomi";else if(/BlackBerry/i.test(a.ua)){a.name="BlackBerry";if(b=a.ua.match(/blackberry ?([.0-9a-zA-Z]+)/i))a.name+=
" "+b[1];a.image="blackberry"}else if(/Coolpad/i.test(a.ua)){a.name="CoolPad";if(b=a.ua.match(/CoolPad( |\_)?([.0-9a-zA-Z]+)/i))a.name+=" "+b[2];a.image="coolpad"}else if(/Dell Streak/i.test(a.ua))a.name="Dell Streak",a.image="dell";else if(/Dell/i.test(a.ua))a.name="Dell",a.image="dell";else if(/Desire/i.test(a.ua))a.name="HTC Desire",a.image="htc";else if(/Rhodium/i.test(a.ua)||/HTC[_|\ ]Touch[_|\ ]Pro2/i.test(a.ua)||/WMD-50433/i.test(a.ua))a.name="HTC Touch Pro2",a.image="htc";else if(/HTC[_|\ ]Touch[_|\ ]Pro/i.test(a.ua))a.name=
"HTC Touch Pro",a.image="htc";else if(/Windows Phone .+ by HTC/i.test(a.ua)){a.name="HTC";if(b=a.ua.match(/Windows Phone ([0-9A-Za-z]+) by HTC/i))a.name+=" "+b[1];a.image="htc"}else if(/HTC/i.test(a.ua)){a.name="HTC";if(b=a.ua.match(/HTC[\ |_|-]?([.0-9a-zA-Z]+)/i))a.name+=" "+b[1];else if(b=a.ua.match(/HTC([._0-9a-zA-Z]+)/i))a.name+=b[1].repalce(/_/g," ");a.image="htc"}else if(/Huawei/i.test(a.ua)){if(a.name="Huawei",a.image="huawei",b=a.ua.match(/HUAWEI( |\_)?([.0-9a-zA-Z]+)/i))a.name+=" "+b[2]}else if(/Kindle/i.test(a.ua)){a.name=
"Kindle";if(b=a.ua.match(/Kindle\/([.0-9a-zA-Z]+)/i))a.name+=" "+b[1];a.image="kindle"}else if(/k-touch/i.test(a.ua)){if(a.name="K-Touch",a.image="k-touch",b=a.ua.match(/k-touch[ _]([.0-9a-zA-Z]+)/i))a.name+=" "+b[1]}else if(/Lenovo|lepad/i.test(a.ua))a.name="Lenovo",(b=a.ua.match(/Lenovo[\ |\-|\/|\_]([.0-9a-zA-Z]+)/i))?a.name+=" "+b[1]:/lepad/i.test(a.ua)&&(a.name+=" LePad"),a.image="lenovo";else if(/LG/i.test(a.ua)){a.name="LG";if(b=a.ua.match(/LGE?([- \/])([0-9a-zA-Z]+)/i))a.name+=" "+b[2];a.image=
"lg"}else if(/\ Droid/i.test(a.ua))a.name+="Motorola Droid",a.image="motorola";else if(/XT720/i.test(a.ua))a.name+="Motorola XT720",a.image="motorola";else if(/MOT-/i.test(a.ua)||/MIB/i.test(a.ua)){a.name="Motorola";if(b=a.ua.match(/MOTO([.0-9a-zA-Z]+)/i))a.name+=" "+b[1];if(b=a.ua.match(/MOT-([.0-9a-zA-Z]+)/i))a.name+=" "+b[1];a.image="motorola"}else if(/XOOM/i.test(a.ua))a.name+="Motorola Xoom",a.image="motorola";else if(/Nintendo/i.test(a.ua))a.name="Nintendo",a.image="nintendo",/Nintendo DSi/i.test(a.ua)?
(a.name+=" DSi",a.image="nintendodsi"):/Nintendo DS/i.test(a.ua)?(a.name+=" DS",a.image="nintendods"):/Nintendo WiiU/i.test(a.ua)?(a.name+=" Wii U",a.image="nintendowiiu"):/Nintendo Wii/i.test(a.ua)&&(a.name+=" Wii",a.image="nintendowii");else if(/Nokia/i.test(a.ua))if(a.name="Nokia",a.image="nokia",b=a.ua.match(/Nokia(E|N| )?([0-9]+)/i))/IEMobile|WPDesktop/i.test(a.ua)?("909"==b[2]&&(b[2]="1020"),a.name+=" Lumia "+b[2]):a.name+=" "+("undefined"===typeof b[1]?"":b[1])+b[2];else{if(b=a.ua.match(/Lumia ([0-9]+)/i))a.name+=
" Lumia "+b[1]}else if(/onda/i.test(a.ua))a.name="Onda",a.image="onda";else if(/oppo/i.test(a.ua))a.name="OPPO",a.image="oppo";else if(/\ Pixi\//i.test(a.ua))a.name="Palm Pixi",a.image="palm";else if(/\ Pre\//i.test(a.ua))a.name="Palm Pre",a.image="palm";else if(/Palm/i.test(a.ua))a.name="Palm",a.image="palm";else if(/webos/i.test(a.ua))a.name="Palm",a.image="palm";else if(/PlayStation/i.test(a.ua))a.name="PlayStation",/[PS|PlayStation\ ]3/i.test(a.ua)?a.name+=" 3":/[PS|PlayStation\ ]4/i.test(a.ua)?
a.name+=" 4":/PlayStation Portable|PSP/i.test(a.ua)?a.name+=" Portable":/PlayStation Vita|PSVita/i.test(a.ua)&&(a.name+=" Vita"),a.image="playstation";else if(/Galaxy Nexus/i.test(a.ua))a.name="Galaxy Nexus",a.image="samsung";else if(/Smart-?TV/i.test(a.ua))a.name="Samsung Smart TV",a.image="samsung";else if(/GT-/i.test(a.ua)){a.name="Samsung";if(b=a.ua.match(/GT-([.\-0-9a-zA-Z]+)/i))a.name+=" "+b[1];a.image="samsung"}else if(/Samsung/i.test(a.ua)){a.name="Samsung";if(b=a.ua.match(/Samsung-([.\-0-9a-zA-Z]+)/i))a.name+=
" "+b[1];a.image="samsung"}else if(/SonyEricsson/i.test(a.ua)){a.name="SonyEricsson";if(b=a.ua.match(/SonyEricsson([.0-9a-zA-Z]+)/i))a.name+=" "+b[1];a.image="sonyericsson"}else if(/vivo/i.test(a.ua)){if(a.name="vivo",a.image="vivo",b=a.ua.match(/VIVO ([.0-9a-zA-Z]+)/i))a.name+=" "+b[1]}else if(/Xperia/i.test(a.ua)){if(a.name="Xperia",a.image="xperia",b=a.ua.match(/Xperia(-T)?( |\_|\-)?([.0-9a-zA-Z]+)/i))a.name+=" "+b[3]}else if(/zte/i.test(a.ua)){if(a.name="ZTE",a.image="zte",b=a.ua.match(/ZTE(-T)?( |\_|\-)?([.0-9a-zA-Z]+)/i))a.name+=
" "+b[3]}else if(/Ubuntu\;\ Mobile/i.test(a.ua))a.name="Ubuntu Phone",a.image="ubuntutouch";else if(/Ubuntu\;\ Tablet/i.test(a.ua))a.name="Ubuntu Tablet",a.image="ubuntutouch";else if(/Nexus/i.test(a.ua)){if(a.name="Nexus",a.image="google-nexusone",b=a.ua.match(/Nexus ([.0-9a-zA-Z]+)/i))a.name+=" "+b[1]}else if(/iPad/i.test(a.ua)){a.name="iPad";if(b=a.ua.match(/CPU\ OS\ ([._0-9a-zA-Z]+)/i))a.name+=" iOS "+b[1].replace(/_/g,".");a.image="ipad"}else if(/iPod/i.test(a.ua)){a.name="iPod";if(b=a.ua.match(/iPhone\ OS\ ([._0-9a-zA-Z]+)/i))a.name+=
" iOS "+b[1].replace(/_/g,".");a.image="iphone"}else if(/iPhone/i.test(a.ua)){a.name="iPhone";if(b=a.ua.match(/iPhone\ OS\ ([._0-9a-zA-Z]+)/i))a.name+=" iOS "+b[1].replace(/_/g,".");a.image="iphone"}else/MSIE.+?Windows.+?Trident/i.test(a.ua)&&!/Windows ?Phone/i.test(a.ua)&&(a.name=""),a.image="null";a.full=a.name;return a}};"undefined"!==typeof module&&module.exports?module.exports=e:"undefined"!==typeof define&&define.amd?define([],function(){return e}):"undefined"!==typeof define&&define.cmd?define([],
function(a,b,g){g.exports=e}):(h.USERAGENT_DEVICE=function(){},USERAGENT_DEVICE.prototype.analyze=e.analyze)})(this);
(function(h){var e={analyze:function(a){a={ua:a,name:"",version:"",full:"",windows:!1,linux:!1,x64:!1,dir:"os"};/x86_64|Win64; x64|WOW64/i.test(a.ua)&&(a.x64=!0);if(/Windows|Win(NT|32|95|98|16)|ZuneWP7|WPDesktop/i.test(a.ua)){a.windows=!0;a.full="Windows";a.name="Windows";a.image="win-2";a.version="";var b=null;if(/Windows Phone|WPDesktop|ZuneWP7|WP7/i.test(a.ua))a.name+=" Phone",a.image="windowsphone",b=a.ua.match(/Windows Phone (OS )?([0-9\.]+)/i),null!==b&&(a.version=b[2],7===parseInt(a.version)&&
(a.image="wp7")),a.full=a.name+(""===a.version?"":" "+a.version);else if(/Windows NT/i.test(a.ua)){if(a.name="Windows NT",b=a.ua.match(/Windows NT ([.0-9]+)/i),null!==b){switch(b[1]){case "6.4":case "10.0":a.full="Windows 10";a.image="win-5";break;case "6.3":a.full="Windows 8.1";a.image="win-5";break;case "6.2":a.full="Windows 8";a.image="win-5";break;case "6.1":a.full="Windows 7";a.image="win-4";break;case "6.0":a.full="Windows Vista";a.image="win-3";break;case "5.2":a.full="Windows Server 2003";
a.image="win-2";break;case "5.1":a.full="Windows XP";a.image="win-2";break;case "5.01":a.full="Windows 2000 Service Pack 1";a.image="win-1";break;case "5.0":a.full="Windows 2000";a.image="win-1";break;case "4.0":a.full="Windows NT 4.0";a.image="win-1";break;case "3.51":a.full="Windows NT 3.11",a.image="win-1"}a.version=b[1]}}else/Windows XP/i.test(a.ua)?(a.version="5.1",a.name="Windows NT",a.full="Windows XP",a.image="win-2"):/Windows 2000/i.test(a.ua)?(a.version="5.0",a.name="Windows NT",a.full=
"Windows 2000",a.image="win-1"):/WinNT4.0/i.test(a.ua)?(a.version="4.0",a.name="Windows NT",a.full="Windows NT 4.0",a.image="win-1"):/WinNT3.51/i.test(a.ua)?(a.version="3.51",a.name="Windows NT",a.full="Windows NT 3.11",a.image="win-1"):/Win(dows )?3.11|Win16/i.test(a.ua)?(a.full+=" 3.11",a.image="win-1"):/Windows 3.1/i.test(a.ua)?(a.full+=" 3.1",a.image="win-1"):/Win 9x 4.90|Windows ME/i.test(a.ua)?(a.full+=" Me",a.image="win-1"):/Win98/i.test(a.ua)?(a.full+=" 98 SE",a.image="win-1"):/Windows (98|4\.10)/i.test(a.ua)?
(a.full+=" 98",a.image="win-1"):/Windows 95/i.test(a.ua)||/Win95/i.test(a.ua)?(a.full+=" 95",a.image="win-1"):/Windows CE|Windows .+Mobile/i.test(a.ua)?(a.full+=" CE",a.image="win-2"):/WM5/i.test(a.ua)?(a.name+=" Mobile",a.version="5",a.full=a.name+" "+a.version,a.image="win-phone"):/WindowsMobile/i.test(a.ua)&&(a.name+=" Mobile",a.full=a.name,a.image="win-phone")}else if(/Linux/i.test(a.ua)&&!/Android|ADR/.test(a.ua)){a.linux=!0;a.name="";a.image="";a.version="";b=null;if(/[^A-Za-z]Arch/i.test(a.ua))a.name=
"Arch Linux",a.image="archlinux";else if(/CentOS/i.test(a.ua)){a.name="CentOS";if(b=a.ua.match(/.el([.0-9a-zA-Z]+).centos/i))a.version=b[1];a.image="centos"}else if(/Chakra/i.test(a.ua))a.name="Chakra Linux",a.image="chakra";else if(/Crunchbang/i.test(a.ua))a.name="Crunchbang",a.image="crunchbang";else if(/Debian/i.test(a.ua))a.name="Debian GNU/Linux",a.image="debian";else if(/Edubuntu/i.test(a.ua)){a.name="Edubuntu";if(b=a.ua.match(/Edubuntu[\/|\ ]([.0-9a-zA-Z]+)/i))a.version=b[1];10>parseInt(a.version)?
a.image="edubuntu-1":a.image="edubuntu-2"}else if(/Fedora/i.test(a.ua)){a.name="Fedora";if(b=a.ua.match(/.fc([.0-9a-zA-Z]+)/i))a.version=b[1];a.image="fedora"}else if(/Foresight\ Linux/i.test(a.ua)){a.name="Foresight Linux";if(b=a.ua.match(/Foresight\ Linux\/([.0-9a-zA-Z]+)/i))a.version=b[1];a.image="foresight"}else if(/Gentoo/i.test(a.ua))a.name="Gentoo",a.image="gentoo";else if(/Kanotix/i.test(a.ua))a.name="Kanotix",a.image="kanotix";else if(/Knoppix/i.test(a.ua))a.name="Knoppix",a.image="knoppix";
else if(/Kubuntu/i.test(a.ua))a.name="Kubuntu",(b=a.ua.match(/Kubuntu[\/|\ ]([.0-9]+)/i))?(a.version=b[1],10>parseInt(a.version)?a.image="kubuntu-1":a.image="kubuntu-2"):a.image="kubuntu-2";else if(/LindowsOS/i.test(a.ua))a.name="LindowsOS",a.image="lindowsos";else if(/Linspire/i.test(a.ua))a.name="Linspire",a.image="lindowsos";else if(/Linux\ Mint/i.test(a.ua)){a.name="Linux Mint";if(b=a.ua.match(/Linux\ Mint\/([.0-9a-zA-Z]+)/i))a.version=b[1];a.image="linuxmint"}else if(/Lubuntu/i.test(a.ua)){a.name=
"Lubuntu";if(b=a.ua.match(/Lubuntu[\/|\ ]([.0-9a-zA-Z]+)/i))a.version=b[1];10>parseInt(a.version)?a.image="lubuntu-1":a.image="lubuntu-2"}else if(/Mageia/i.test(a.ua))a.name="Mageia",a.image="mageia";else if(/Mandriva/i.test(a.ua)){a.name="Mandriva";if(b=a.ua.match(/mdv([.0-9a-zA-Z]+)/i))a.version=b[1];a.image="mandriva"}else if(/moonOS/i.test(a.ua)){a.name="moonOS";if(b=a.ua.match(/moonOS\/([.0-9a-zA-Z]+)/i))a.version=b[1];a.image="moonos"}else if(/Nova/i.test(a.ua)){a.name="Nova";if(b=a.ua.match(/Nova[\/|\ ]([.0-9a-zA-Z]+)/i))a.version=
b[1];a.image="nova"}else if(/Oracle/i.test(a.ua))a.name="Oracle",(b=a.ua.match(/.el([._0-9a-zA-Z]+)/i))?(a.name+=" Enterprise Linux",a.version=b[1].replace(/_/g,".")):a.name+=" Linux",a.image="oracle";else if(/Pardus/i.test(a.ua))a.name="Pardus",a.image="pardus";else if(/PCLinuxOS/i.test(a.ua)){a.name="PCLinuxOS";if(b=a.ua.match(/PCLinuxOS\/[.\-0-9a-zA-Z]+pclos([.\-0-9a-zA-Z]+)/i))a.version=b[1].replace(/_/g,".");a.image="pclinuxos"}else if(/Red\ Hat/i.test(a.ua)||/RedHat/i.test(a.ua)){a.name="Red Hat";
if(b=a.ua.match(/.el([._0-9a-zA-Z]+)/i))a.name+=" Enterprise Linux",a.version=b[1].replace(/_/g,".");a.image="red-hat"}else if(/Rosa/i.test(a.ua))a.name="Rosa Linux",a.image="rosa";else if(/Sabayon/i.test(a.ua))a.name="Sabayon Linux",a.image="sabayon";else if(/Slackware/i.test(a.ua))a.name="Slackware",a.image="slackware";else if(/Suse/i.test(a.ua))a.name="openSUSE",a.image="suse";else if(/VectorLinux/i.test(a.ua))a.name="VectorLinux",a.image="vectorlinux";else if(/Venenux/i.test(a.ua))a.name="Venenux GNU Linux",
a.image="venenux";else if(/Xandros/i.test(a.ua))a.name="Xandros",a.image="xandros";else if(/Xubuntu/i.test(a.ua)){a.name="Xubuntu";if(b=a.ua.match(/Xubuntu[\/|\ ]([.0-9a-zA-Z]+)/i))a.version=b[1];10>parseInt(a.version)?a.image="xubuntu-1":a.image="xubuntu-2"}else if(/Zenwalk/i.test(a.ua))a.name="Zenwalk GNU Linux",a.image="zenwalk";else if(/Ubuntu/i.test(a.ua)){a.name="Ubuntu";if(b=a.ua.match(/Ubuntu[\/|\ ]([.0-9]+[.0-9a-zA-Z]+)/i))a.version=b[1],10>parseInt(a.version)&&(a.image="ubuntu-1");""===
a.image&&(a.image="ubuntu-2")}else a.name="GNU/Linux",a.image="linux";a.full=a.name;""!==a.version&&(a.full+=" "+a.version)}else{a.name="";a.image="";a.version="";a.full="";if(/Android|ADR /i.test(a.ua)){if(a.name="Android",a.image="android",rep=a.ua.match(/(Android|Adr)[\ |\/]?([.0-9a-zA-Z]+)/i))a.version=rep[2]}else if(/AmigaOS/i.test(a.ua)){a.name="AmigaOS";if(rep=a.ua.match(/AmigaOS\ ([.0-9a-zA-Z]+)/i))a.version=rep[1];a.image="amigaos"}else if(/BB10/i.test(a.ua))a.name="BlackBerry OS 10",a.image=
"blackberry";else if(/BeOS/i.test(a.ua))a.name="BeOS",a.image="beos";else if(/\b(?!Mi)CrOS(?!oft)/i.test(a.ua))a.name="Google Chrome OS",a.image="chromeos";else if(/DragonFly/i.test(a.ua))a.name="DragonFly BSD",a.image="dragonflybsd";else if(/FreeBSD/i.test(a.ua))a.name="FreeBSD",a.image="freebsd";else if(/Inferno/i.test(a.ua))a.name="Inferno",a.image="inferno";else if(/IRIX/i.test(a.ua)){a.name="IRIX";if(rep=a.ua.match(/IRIX(64)?\ ([.0-9a-zA-Z]+)/i))void 0!==rep[1]&&""!==rep[1]&&(a.x64=!0),void 0!==
rep[2]&&""!==rep[2]&&(a.version=rep[2]);a.image="irix"}else if(/Mac/i.test(a.ua)||/Darwin/i.test(a.ua))(rep=a.ua.match(/(Mac OS ?X)/i))?(a.version=a.ua.substr(a.ua.toLowerCase().indexOf(rep[1].toLowerCase())),a.version=a.version.substr(0,a.version.indexOf(")")),0<a.version.indexOf(";")&&(a.version=a.version.substr(0,a.version.indexOf(";"))),a.version=a.version.replace(/_/g,"."),a.version=a.version.replace(/Mac OS ?X ?/,""),a.name="Mac OS X",a.full=a.name+" "+a.version,a.image="Mac OSX"==rep[1]?"mac-2":
"mac-3"):(/Darwin/i.test(a.ua)?a.name="Mac OS Darwin":a.name="Macintosh",a.image="mac-1");else if(/MorphOS/i.test(a.ua))a.name="MorphOS",a.image="morphos";else if(/NetBSD/i.test(a.ua))a.name="NetBSD",a.image="netbsd";else if(/OpenBSD/i.test(a.ua))a.name="OpenBSD",a.image="openbsd";else if(/RISC OS/i.test(a.ua)){if(a.name="RISC OS",a.image="risc",rep=a.ua.match(/RISC OS ([.0-9a-zA-Z]+)/i))a.version=rep[1]}else if(/Solaris|SunOS/i.test(a.ua))a.name="Solaris",a.image="solaris";else if(/Symb(ian)?(OS)?/i.test(a.ua)){a.name=
"SymbianOS";if(rep=a.ua.match(/Symb(ian)?(OS)?\/([.0-9a-zA-Z]+)/i))a.version=rep[3];a.image="symbian"}else/Unix/i.test(a.ua)?(a.name="Unix",a.image="unix"):/webOS/i.test(a.ua)?(a.name="Palm webOS",a.image="palm"):/J2ME\/MIDP/i.test(a.ua)?(a.name="J2ME/MIDP Device",a.image="java"):(a.name="Unknown",a.image="null");a.full=a.name+(""===a.version?"":" "+a.version)}""===a.full&&(a.full=a.name);a.x64&&(a.full+=" x64");return a}};"undefined"!==typeof module&&module.exports?module.exports=e:"undefined"!==
typeof define&&define.amd?define([],function(){return e}):"undefined"!==typeof define&&define.cmd?define([],function(a,b,g){g.exports=e}):(h.USERAGENT_OS=function(){},USERAGENT_OS.prototype.analyze=e.analyze)})(this);
(function(h){var e="undefined"!==typeof define&&define.cmd,a="undefined"!==typeof define&&define.amd,b={version:"0.1",publishDate:"20150217",analyze:function(a){var b={};b.ua=a;"undefined"!==typeof this.osDetect&&(b.os=this.osDetect.analyze(a));"undefined"!==typeof this.deviceDetect&&(b.device=this.deviceDetect.analyze(a));"undefined"!==typeof this.browserDetect&&(b.browser=this.browserDetect.analyze(a));"undefined"!==typeof b.device&&(b.platform=b.device);"undefined"!==typeof b.os&&""===b.device.name&&
(b.platform=b.os);return b}},g=function(a,b,c,e){a.osDetect=b;a.deviceDetect=c;a.browserDetect=e};"undefined"!==typeof module&&module.exports?(g(b,require("./lib/os"),require("./lib/device"),require("./lib/browser")),module.exports=b):a?define(["./lib/os","./lib/device","./lib/browser"],function(a,e,c){g(b,a,e,c);return b}):e?define(function(a,e,c){g(b,a("./lib/os"),a("./lib/device"),a("./lib/browser"));c.exports=b}):("undefined"!==typeof USERAGENT_OS&&(b.osDetect=new USERAGENT_OS),"undefined"!==
typeof USERAGENT_DEVICE&&(b.deviceDetect=new USERAGENT_DEVICE),"undefined"!==typeof USERAGENT_BROWSER&&(b.browserDetect=new USERAGENT_BROWSER),h.USERAGENT=b)})(this);;
(function(){
	var _ = this.series,
		jsonpID = 0,
		document = window.document,
		key,
		name,
		rscript = /<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>/gi,
		scriptTypeRE = /^(?:text|application)\/javascript/i,
		xmlTypeRE = /^(?:text|application)\/xml/i,
		jsonType = 'application/json',
		htmlType = 'text/html',
		blankRE = /^\s*$/
	
	var ajax = function(options){
	  var settings = extend({}, options || {})
	  for (key in ajax.settings) if (settings[key] === undefined) settings[key] = ajax.settings[key]
	
	  ajaxStart(settings)
	
	  if (!settings.crossDomain) settings.crossDomain = /^([\w-]+:)?\/\/([^\/]+)/.test(settings.url) &&
		RegExp.$2 != window.location.host
	
	  var dataType = settings.dataType, hasPlaceholder = /=\?/.test(settings.url)
	  if (dataType == 'jsonp' || hasPlaceholder) {
		if (!hasPlaceholder) settings.url = appendQuery(settings.url, 'callback=?')
		return ajax.JSONP(settings)
	  }
	
	  if (!settings.url) settings.url = window.location.toString()
	  serializeData(settings)
	
	  var mime = settings.accepts[dataType],
		  baseHeaders = { },
		  protocol = /^([\w-]+:)\/\//.test(settings.url) ? RegExp.$1 : window.location.protocol,
		  xhr = ajax.settings.xhr(), abortTimeout
	
	  if (!settings.crossDomain) baseHeaders['X-Requested-With'] = 'XMLHttpRequest'
	  if (mime) {
		baseHeaders['Accept'] = mime
		if (mime.indexOf(',') > -1) mime = mime.split(',', 2)[0]
		xhr.overrideMimeType && xhr.overrideMimeType(mime)
	  }
	  if (settings.contentType || (settings.data && settings.type.toUpperCase() != 'GET'))
		baseHeaders['Content-Type'] = (settings.contentType || 'application/x-www-form-urlencoded')
	  settings.headers = extend(baseHeaders, settings.headers || {})
	
	  xhr.onreadystatechange = function(){
		if (xhr.readyState == 4) {
		  clearTimeout(abortTimeout)
		  var result, error = false
		  if ((xhr.status >= 200 && xhr.status < 300) || xhr.status == 304 || (xhr.status == 0 && protocol == 'file:')) {
			dataType = dataType || mimeToDataType(xhr.getResponseHeader('content-type'))
			result = xhr.responseText
	
			try {
			  if (dataType == 'script')    (1,eval)(result)
			  else if (dataType == 'xml')  result = xhr.responseXML
			  else if (dataType == 'json') result = blankRE.test(result) ? null : JSON.parse(result)
			} catch (e) { error = e }
	
			if (error) ajaxError(error, 'parsererror', xhr, settings)
			else ajaxSuccess(result, xhr, settings)
		  } else {
			ajaxError(null, 'error', xhr, settings)
		  }
		}
	  }
	
	  var async = 'async' in settings ? settings.async : true
	  xhr.open(settings.type, settings.url, async)
	
	  for (name in settings.headers) xhr.setRequestHeader(name, settings.headers[name])
	
	  if (ajaxBeforeSend(xhr, settings) === false) {
		xhr.abort()
		return false
	  }
	
	  if (settings.timeout > 0) abortTimeout = setTimeout(function(){
		  xhr.onreadystatechange = empty
		  xhr.abort()
		  ajaxError(null, 'timeout', xhr, settings)
		}, settings.timeout)
	
	  // avoid sending empty string (#319)
	  xhr.send(settings.data ? settings.data : null)
	  return xhr
	}
	
	
	// trigger a custom event and return false if it was cancelled
	function triggerAndReturn(context, eventName, data) {
	  //todo: Fire off some events
	  //var event = $.Event(eventName)
	  //$(context).trigger(event, data)
	  return true;//!event.defaultPrevented
	}
	
	// trigger an Ajax "global" event
	function triggerGlobal(settings, context, eventName, data) {
	  if (settings.global) return triggerAndReturn(context || document, eventName, data)
	}
	
	// Number of active Ajax requests
	ajax.active = 0
	
	function ajaxStart(settings) {
	  if (settings.global && ajax.active++ === 0) triggerGlobal(settings, null, 'ajaxStart')
	}
	function ajaxStop(settings) {
	  if (settings.global && !(--ajax.active)) triggerGlobal(settings, null, 'ajaxStop')
	}
	
	// triggers an extra global event "ajaxBeforeSend" that's like "ajaxSend" but cancelable
	function ajaxBeforeSend(xhr, settings) {
	  var context = settings.context
	  if (settings.beforeSend.call(context, xhr, settings) === false ||
		  triggerGlobal(settings, context, 'ajaxBeforeSend', [xhr, settings]) === false)
		return false
	
	  triggerGlobal(settings, context, 'ajaxSend', [xhr, settings])
	}
	function ajaxSuccess(data, xhr, settings) {
	  var context = settings.context, status = 'success'
	  settings.success.call(context, data, status, xhr)
	  triggerGlobal(settings, context, 'ajaxSuccess', [xhr, settings, data])
	  ajaxComplete(status, xhr, settings)
	}
	// type: "timeout", "error", "abort", "parsererror"
	function ajaxError(error, type, xhr, settings) {
	  var context = settings.context
	  settings.error.call(context, xhr, type, error)
	  triggerGlobal(settings, context, 'ajaxError', [xhr, settings, error])
	  ajaxComplete(type, xhr, settings)
	}
	// status: "success", "notmodified", "error", "timeout", "abort", "parsererror"
	function ajaxComplete(status, xhr, settings) {
	  var context = settings.context
	  settings.complete.call(context, xhr, status)
	  triggerGlobal(settings, context, 'ajaxComplete', [xhr, settings])
	  ajaxStop(settings)
	}
	
	// Empty function, used as default callback
	function empty() {}
	
	ajax.JSONP = function(options){
	  if (!('type' in options)) return ajax(options)
	
	  var callbackName = 'jsonp' + (++jsonpID),
		script = document.createElement('script'),
		abort = function(){
		  //todo: remove script
		  //$(script).remove()
		  if (callbackName in window) window[callbackName] = empty
		  ajaxComplete('abort', xhr, options)
		},
		xhr = { abort: abort }, abortTimeout,
		head = document.getElementsByTagName("head")[0]
		  || document.documentElement
	
	  if (options.error) script.onerror = function() {
		xhr.abort()
		options.error()
	  }
	
	  window[callbackName] = function(data){
		clearTimeout(abortTimeout)
		  //todo: remove script
		  //$(script).remove()
		delete window[callbackName]
		ajaxSuccess(data, xhr, options)
	  }
	
	  serializeData(options)
	  script.src = options.url.replace(/=\?/, '=' + callbackName)
	
	  // Use insertBefore instead of appendChild to circumvent an IE6 bug.
	  // This arises when a base node is used (see jQuery bugs #2709 and #4378).
	  head.insertBefore(script, head.firstChild);
	
	  if (options.timeout > 0) abortTimeout = setTimeout(function(){
		  xhr.abort()
		  ajaxComplete('timeout', xhr, options)
		}, options.timeout)
	
	  return xhr
	}
	
	ajax.settings = {
	  // Default type of request
	  type: 'GET',
	  // Callback that is executed before request
	  beforeSend: empty,
	  // Callback that is executed if the request succeeds
	  success: empty,
	  // Callback that is executed the the server drops error
	  error: empty,
	  // Callback that is executed on request complete (both: error and success)
	  complete: empty,
	  // The context for the callbacks
	  context: null,
	  // Whether to trigger "global" Ajax events
	  global: true,
	  // Transport
	  xhr: function () {
		return new window.XMLHttpRequest()
	  },
	  // MIME types mapping
	  accepts: {
		script: 'text/javascript, application/javascript',
		json:   jsonType,
		xml:    'application/xml, text/xml',
		html:   htmlType,
		text:   'text/plain'
	  },
	  // Whether the request is to another domain
	  crossDomain: false,
	  // Default timeout
	  timeout: 0
	}
	
	function mimeToDataType(mime) {
	  return mime && ( mime == htmlType ? 'html' :
		mime == jsonType ? 'json' :
		scriptTypeRE.test(mime) ? 'script' :
		xmlTypeRE.test(mime) && 'xml' ) || 'text'
	}
	
	function appendQuery(url, query) {
	  return (url + '&' + query).replace(/[&?]{1,2}/, '?')
	}
	
	// serialize payload and append it to the URL for GET requests
	function serializeData(options) {
	  if (_.isObject(options.data)) options.data = param(options.data)
	  if (options.data && (!options.type || options.type.toUpperCase() == 'GET'))
		options.url = appendQuery(options.url, options.data)
	}
	
	ajax.get = function(url, success){ return ajax({ url: url, success: success }) }
	
	ajax.post = function(url, data, success, dataType){
	  if (_.isFunction(data)) dataType = dataType || success, success = data, data = null
	  return ajax({ type: 'POST', url: url, data: data, success: success, dataType: dataType })
	}
	
	ajax.getJSON = function(url, success){
	  return ajax({ url: url, success: success, dataType: 'json' })
	}
	
	var escape = encodeURIComponent
	
	function serialize(params, obj, traditional, scope){
	  var array = _.isArray(obj);
	  for (var key in obj) {
		var value = obj[key];
	
		if (scope) key = traditional ? scope : scope + '[' + (array ? '' : key) + ']'
		// handle data in serializeArray() format
		if (!scope && array) params.add(value.name, value.value)
		// recurse into nested objects
		else if (traditional ? _.isArray(value) : _.isObject(value))
		  serialize(params, value, traditional, key)
		else params.add(key, value)
	  }
	}
	
	function param(obj, traditional){
	  var params = []
	  params.add = function(k, v){ this.push(escape(k) + '=' + escape(v)) }
	  serialize(params, obj, traditional)
	  return params.join('&').replace('%20', '+')
	}
	
	function extend(target) {
	  var slice = Array.prototype.slice;
	  slice.call(arguments, 1).forEach(function(source) {
		for (key in source)
		  if (source[key] !== undefined)
			target[key] = source[key]
	  })
	  return target
	}
	
	this.series.ajax = ajax;
	
}).call(this);;
// Copyright Joyent, Inc. and other Node contributors.
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the
// following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN
// NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE
// USE OR OTHER DEALINGS IN THE SOFTWARE.
(function(){
	'use strict';
	
	// Query String Utilities
	
	var QueryString = {};
	var util = this.series;
	
	
	// If obj.hasOwnProperty has been overridden, then calling
	// obj.hasOwnProperty(prop) will break.
	// See: https://github.com/joyent/node/issues/1707
	function hasOwnProperty(obj, prop) {
		return Object.prototype.hasOwnProperty.call(obj, prop);
	}
	
	
	function charCode(c) {
		return c.charCodeAt(0);
	}
	
	
	// a safe fast alternative to decodeURIComponent
	QueryString.unescapeBuffer = function(s, decodeSpaces) {
		var out = new Buffer(s.length);
		var state = 'CHAR'; // states: CHAR, HEX0, HEX1
		var n, m, hexchar;
	
		for (var inIndex = 0, outIndex = 0; inIndex <= s.length; inIndex++) {
			var c = s.charCodeAt(inIndex);
			switch (state) {
				case 'CHAR':
					switch (c) {
						case charCode('%'):
							n = 0;
							m = 0;
							state = 'HEX0';
							break;
						case charCode('+'):
							if (decodeSpaces) c = charCode(' ');
							// pass thru
						default:
							out[outIndex++] = c;
							break;
					}
					break;
	
				case 'HEX0':
					state = 'HEX1';
					hexchar = c;
					if (charCode('0') <= c && c <= charCode('9')) {
						n = c - charCode('0');
					} else if (charCode('a') <= c && c <= charCode('f')) {
						n = c - charCode('a') + 10;
					} else if (charCode('A') <= c && c <= charCode('F')) {
						n = c - charCode('A') + 10;
					} else {
						out[outIndex++] = charCode('%');
						out[outIndex++] = c;
						state = 'CHAR';
						break;
					}
					break;
	
				case 'HEX1':
					state = 'CHAR';
					if (charCode('0') <= c && c <= charCode('9')) {
						m = c - charCode('0');
					} else if (charCode('a') <= c && c <= charCode('f')) {
						m = c - charCode('a') + 10;
					} else if (charCode('A') <= c && c <= charCode('F')) {
						m = c - charCode('A') + 10;
					} else {
						out[outIndex++] = charCode('%');
						out[outIndex++] = hexchar;
						out[outIndex++] = c;
						break;
					}
					out[outIndex++] = 16 * n + m;
					break;
			}
		}
	
		// TODO support returning arbitrary buffers.
	
		return out.slice(0, outIndex - 1);
	};
	
	
	QueryString.unescape = function(s, decodeSpaces) {
		try {
			return decodeURIComponent(s);
		} catch (e) {
			return QueryString.unescapeBuffer(s, decodeSpaces).toString();
		}
	};
	
	
	QueryString.escape = function(str) {
		return encodeURIComponent(str);
	};
	
	var stringifyPrimitive = function(v) {
		if (util.isString(v))
			return v;
		if (util.isBoolean(v))
			return v ? 'true' : 'false';
		if (util.isNumber(v))
			return isFinite(v) ? v : '';
		return '';
	};
	
	
	QueryString.stringify = QueryString.encode = function(obj, sep, eq, options) {
		sep = sep || '&';
		eq = eq || '=';
	
		var encode = QueryString.escape;
		if (options && typeof options.encodeURIComponent === 'function') {
			encode = options.encodeURIComponent;
		}
	
		if (util.isObject(obj)) {
			var keys = Object.keys(obj);
			var fields = [];
	
			for (var i = 0; i < keys.length; i++) {
				var k = keys[i];
				var v = obj[k];
				var ks = encode(stringifyPrimitive(k)) + eq;
	
				if (util.isArray(v)) {
					for (var j = 0; j < v.length; j++)
						fields.push(ks + encode(stringifyPrimitive(v[j])));
				} else {
					fields.push(ks + encode(stringifyPrimitive(v)));
				}
			}
			return fields.join(sep);
		}
		return '';
	};
	
	// Parse a key=val string.
	QueryString.parse = QueryString.decode = function(qs, sep, eq, options) {
		sep = sep || '&';
		eq = eq || '=';
		var obj = {};
	
		if (!util.isString(qs) || qs.length === 0) {
			return obj;
		}
	
		var regexp = /\+/g;
		qs = qs.split(sep);
	
		var maxKeys = 1000;
		if (options && util.isNumber(options.maxKeys)) {
			maxKeys = options.maxKeys;
		}
	
		var len = qs.length;
		// maxKeys <= 0 means that we should not limit keys count
		if (maxKeys > 0 && len > maxKeys) {
			len = maxKeys;
		}
	
		var decode = QueryString.unescape;
		if (options && typeof options.decodeURIComponent === 'function') {
			decode = options.decodeURIComponent;
		}
	
		for (var i = 0; i < len; ++i) {
			var x = qs[i].replace(regexp, '%20'),
					idx = x.indexOf(eq),
					kstr, vstr, k, v;
	
			if (idx >= 0) {
				kstr = x.substr(0, idx);
				vstr = x.substr(idx + 1);
			} else {
				kstr = x;
				vstr = '';
			}
	
			try {
				k = decode(kstr);
				v = decode(vstr);
			} catch (e) {
				k = QueryString.unescape(kstr, true);
				v = QueryString.unescape(vstr, true);
			}
	
			if (!hasOwnProperty(obj, k)) {
				obj[k] = v;
			} else if (util.isArray(obj[k])) {
				obj[k].push(v);
			} else {
				obj[k] = [obj[k], v];
			}
		}
	
		return obj;
	};
	
	this.series.QueryString = QueryString;
	
}).call(this);;
(function(){
	
	'use strict';
	
	var series = this.series
		,	ua = USERAGENT.analyze(navigator.userAgent);
		
			series.ua = ua.ua;
			series.browser = ua.browser;
			series.os = ua.os;
			series.device = ua.device;

	var isTop, testDiv, scrollIntervalId,
			isBrowser = typeof window !== "undefined" && window.document,
			isPageLoaded = !isBrowser,
			doc = isBrowser ? document : null,
			readyCalls = [];

	function runCallbacks(callbacks) {
			var i;
			for (i = 0; i < callbacks.length; i += 1) {
					callbacks[i](doc);
			}
	}

	function callReady() {
			var callbacks = readyCalls;

			if (isPageLoaded) {
					//Call the DOM ready callbacks
					if (callbacks.length) {
							readyCalls = [];
							runCallbacks(callbacks);
					}
			}
	}

	/**
	 * Sets the page as loaded.
	 */
	function pageLoaded() {
			if (!isPageLoaded) {
					isPageLoaded = true;
					if (scrollIntervalId) {
							clearInterval(scrollIntervalId);
					}

					callReady();
			}
	}

	if (isBrowser) {
			if (document.addEventListener) {
					//Standards. Hooray! Assumption here that if standards based,
					//it knows about DOMContentLoaded.
					document.addEventListener("DOMContentLoaded", pageLoaded, false);
					window.addEventListener("load", pageLoaded, false);
			} else if (window.attachEvent) {
					window.attachEvent("onload", pageLoaded);

					testDiv = document.createElement('div');
					try {
							isTop = window.frameElement === null;
					} catch (e) {}

					//DOMContentLoaded approximation that uses a doScroll, as found by
					//Diego Perini: http://javascript.nwbox.com/IEContentLoaded/,
					//but modified by other contributors, including jdalton
					if (testDiv.doScroll && isTop && window.external) {
							scrollIntervalId = setInterval(function () {
									try {
											testDiv.doScroll();
											pageLoaded();
									} catch (e) {}
							}, 30);
					}
			}

			//Check if document already complete, and if so, just trigger page load
			//listeners. Latest webkit browsers also use "interactive", and
			//will fire the onDOMContentLoaded before "interactive" but not after
			//entering "interactive" or "complete". More details:
			//http://dev.w3.org/html5/spec/the-end.html#the-end
			//http://stackoverflow.com/questions/3665561/document-readystate-of-interactive-vs-ondomcontentloaded
			//Hmm, this is more complicated on further use, see "firing too early"
			//bug: https://github.com/requirejs/domReady/issues/1
			//so removing the || document.readyState === "interactive" test.
			//There is still a window.onload binding that should get fired if
			//DOMContentLoaded is missed.
			if (document.readyState === "complete") {
					pageLoaded();
			}
	}

	/** START OF PUBLIC API **/

	/**
	 * Registers a callback for DOM ready. If DOM is already ready, the
	 * callback is called immediately.
	 * @param {Function} callback
	 */
	function domReady(callback) {
			if (isPageLoaded) {
					callback(doc);
			} else {
					readyCalls.push(callback);
			}
			return domReady;
	}

	domReady.version = '2.0.1';

	/**
	 * Loader Plugin API method
	 */
	domReady.load = function (name, req, onLoad, config) {
			if (config.isBuild) {
					onLoad(null);
			} else {
					domReady(onLoad);
			}
	};

	/** END OF PUBLIC API **/

	series.domReady = domReady;
	
}).call(this);;
// JavaScript Document
(function(){
	var series = this.series
		,	ajax = series.ajax
		,	utils = {}
		,	slice = Array.prototype.slice;
	
	
	(function(exports){
		var regExpChars = /[|\\{}()[\]^$+*?.]/g;

		/**
		 * Escape characters reserved in regular expressions.
		 *
		 * If `string` is `undefined` or `null`, the empty string is returned.
		 *
		 * @param {String} string Input string
		 * @return {String} Escaped string
		 * @static
		 * @private
		 */
		exports.escapeRegExpChars = function (string) {
			// istanbul ignore if
			if (!string) {
				return '';
			}
			return String(string).replace(regExpChars, '\\$&');
		};
		
		var _ENCODE_HTML_RULES = {
					'&': '&amp;'
				, '<': '&lt;'
				, '>': '&gt;'
				, '"': '&#34;'
				, "'": '&#39;'
				}
			, _MATCH_HTML = /[&<>\'"]/g;
		
		/**
		 * Stringified version of constants used by {@link module:utils.escapeXML}.
		 *
		 * It is used in the process of generating {@link ClientFunction}s.
		 *
		 * @readonly
		 * @type {String}
		 */
		
		var escapeFuncStr =
			'var _ENCODE_HTML_RULES = {\n'
		+ '      "&": "&amp;"\n'
		+ '    , "<": "&lt;"\n'
		+ '    , ">": "&gt;"\n'
		+ '    , \'"\': "&#34;"\n'
		+ '    , "\'": "&#39;"\n'
		+ '    }\n'
		+ '  , _MATCH_HTML = /[&<>\'"]/g;\n';
		
		/**
		 * Escape characters reserved in XML.
		 *
		 * If `markup` is `undefined` or `null`, the empty string is returned.
		 *
		 * @implements {EscapeCallback}
		 * @param {String} markup Input string
		 * @return {String} Escaped string
		 * @static
		 * @private
		 */
		exports.escapeXML = function (markup) {
			return markup == undefined
				? ''
				: String(markup)
						.replace(_MATCH_HTML, function(m) {
							return _ENCODE_HTML_RULES[m] || m;
						});
		};
		exports.escapeXML.toString = function () {
			return Function.prototype.toString.call(this) + ';\n' + escapeFuncStr
		};
		
		/**
		 * Copy all properties from one object to another, in a shallow fashion.
		 *
		 * @param  {Object} to   Destination object
		 * @param  {Object} from Source object
		 * @return {Object}      Destination object
		 * @static
		 * @private
		 */
		exports.shallowCopy = function (to, from) {
			from = from || {};
			for (var p in from) {
				to[p] = from[p];
			}
			return to;
		};
		
		/**
		 * Simple in-process cache implementation. Does not implement limits of any
		 * sort.
		 *
		 * @implements Cache
		 * @static
		 * @private
		 */
		exports.cache = {
			_data: {},
			set: function (key, val) {
				this._data[key] = val;
			},
			get: function (key) {
				return this._data[key];
			},
			reset: function () {
				this._data = {};
			}
		};
	})(utils);
	
	var scopeOptionWarned = false
  , _VERSION_STRING = '1.0.1'
  , _DEFAULT_DELIMITER = '%'
  , _DEFAULT_LOCALS_NAME = 'locals'
  , _REGEX_STRING = '(<%%|<%=|<%-|<%#|<%|%>|-%>)'
  , _OPTS = [ 'cache', 'filename', 'delimiter', 'scope', 'context'
            , 'debug', 'compileDebug', 'client', '_with'
            ]
  , _TRAILING_SEMCOL = /;\s*$/
  , _BOM = /^\uFEFF/;
	
	var exports = {};
	
	exports.sep = '/';
	exports.delimiter = ':';
	exports.cache = utils.cache;
	exports.localsName = _DEFAULT_LOCALS_NAME;
	
	exports.templatecache = {};
	
	function handleCache(options, template) {
		var fn
			, path = options.filename
			, hasTemplate = template !== undefined;
	
		if (options.cache) {
			if (!path) {
				throw new Error('cache option requires a filename');
			}
			fn = exports.cache.get(path);
			if (fn) {
				return fn;
			}
			if (!hasTemplate) {
				template = document.getElementById(path).innerHTML.replace(/&lt;/g, '<').replace(/&gt;/g, '>');
				exports.templatecache[path] = template;
			}
		}
		else if (!hasTemplate) {
			// istanbul ignore if: should not happen at all
			if (!path) {
				throw new Error('Internal EJS error: no file name or template '
											+ 'provided');
			}
			template = document.getElementById(path).innerHTML.replace(/&lt;/g, '<').replace(/&gt;/g, '>');
			exports.templatecache[path] = template;
		}
		fn = exports.compile(template, options);
		if (options.cache) {
			exports.cache.set(path, fn);
		}
		return fn;
	}
	
	/**
	 * Compile the given `str` of ejs into a template function.
	 *
	 * @param {String}  template EJS template
	 *
	 * @param {Options} opts     compilation options
	 *
	 * @return {(TemplateFunction|ClientFunction)}
	 * Depending on the value of `opts.client`, either type might be returned.
	 * @public
	 */
	
	exports.compile = function compile(template, opts) {
		var templ;
	
		// v1 compat
		// 'scope' is 'context'
		// FIXME: Remove this in a future version
		if (opts && opts.scope) {
			if (!scopeOptionWarned){
				console.warn('`scope` option is deprecated and will be removed in EJS 3');
				scopeOptionWarned = true;
			}
			if (!opts.context) {
				opts.context = opts.scope;
			}
			delete opts.scope;
		}
		templ = new Template(template, opts);
		return templ.compile();
	};
	
	/**
	 * Get the template function.
	 *
	 * If `options.cache` is `true`, then the template is cached.
	 *
	 * @memberof module:ejs-internal
	 * @param {String}  path    path for the specified file
	 * @param {Options} options compilation options
	 * @return {(TemplateFunction|ClientFunction)}
	 * Depending on the value of `options.client`, either type might be returned
	 * @static
	 */
	
	function includeFile(path, options) {
		var opts = utils.shallowCopy({}, options);
		if (!opts.filename) {
			throw new Error('`include` requires the \'filename\' option.');
		}
		opts.filename = path;
		return handleCache(opts);
	}
	
	/**
	 * Get the JavaScript source of an included file.
	 *
	 * @memberof module:ejs-internal
	 * @param {String}  path    path for the specified file
	 * @param {Options} options compilation options
	 * @return {String}
	 * @static
	 */
	
	function includeSource(path, options) {
		var opts = utils.shallowCopy({}, options)
			, includePath
			, template;
		if (!opts.filename) {
			throw new Error('`include` requires the \'filename\' option.');
		}
		
		includePath = path;
		
		if ( exports.templatecache[includePath] ){
			template = exports.templatecache[includePath];
		}else{
			template = document.getElementById(includePath).innerHTML.replace(/&lt;/g, '<').replace(/&gt;/g, '>');
			exports.templatecache[includePath] = template;
		}
	
		opts.filename = includePath;
		var templ = new Template(template, opts);
		templ.generateSource();
		return templ.source;
	}
	
	/**
	 * Re-throw the given `err` in context to the `str` of ejs, `filename`, and
	 * `lineno`.
	 *
	 * @implements RethrowCallback
	 * @memberof module:ejs-internal
	 * @param {Error}  err      Error object
	 * @param {String} str      EJS source
	 * @param {String} filename file name of the EJS file
	 * @param {String} lineno   line number of the error
	 * @static
	 */
	
	function rethrow(err, str, filename, lineno){
		var lines = str.split('\n')
			, start = Math.max(lineno - 3, 0)
			, end = Math.min(lines.length, lineno + 3);
	
		// Error context
		var context = lines.slice(start, end).map(function (line, i){
			var curr = i + start + 1;
			return (curr == lineno ? ' >> ' : '    ')
				+ curr
				+ '| '
				+ line;
		}).join('\n');
	
		// Alter exception message
		err.path = filename;
		err.message = (filename || 'ejs') + ':'
			+ lineno + '\n'
			+ context + '\n\n'
			+ err.message;
	
		throw err;
	}
	
	/**
	 * Copy properties in data object that are recognized as options to an
	 * options object.
	 *
	 * This is used for compatibility with earlier versions of EJS and Express.js.
	 *
	 * @memberof module:ejs-internal
	 * @param {Object}  data data object
	 * @param {Options} opts options object
	 * @static
	 */
	
	function cpOptsInData(data, opts) {
		_OPTS.forEach(function (p) {
			if (typeof data[p] != 'undefined') {
				opts[p] = data[p];
			}
		});
	}
	
	/**
	 * Render the given `template` of ejs.
	 *
	 * If you would like to include options but not data, you need to explicitly
	 * call this function with `data` being an empty object or `null`.
	 *
	 * @param {String}   template EJS template
	 * @param {Object}  [data={}] template data
	 * @param {Options} [opts={}] compilation and rendering options
	 * @return {String}
	 * @public
	 */
	
	exports.render = function (template, data, opts) {
		data = data || {};
		opts = opts || {};
		var fn;
	
		// No options object -- if there are optiony names
		// in the data, copy them to options
		if (arguments.length == 2) {
			cpOptsInData(data, opts);
		}
	
		fn = handleCache(opts, template);
		return fn.call(opts.context, data);
	};
	
	/**
	 * Render an EJS file at the given `path` and callback `cb(err, str)`.
	 *
	 * If you would like to include options but not data, you need to explicitly
	 * call this function with `data` being an empty object or `null`.
	 *
	 * @param {String}             path     path to the EJS file
	 * @param {Object}            [data={}] template data
	 * @param {Options}           [opts={}] compilation and rendering options
	 * @param {RenderFileCallback} cb callback
	 * @public
	 */
	
	exports.renderFile = function () {
		var args = Array.prototype.slice.call(arguments)
			, path = args.shift()
			, cb = args.pop()
			, data = args.shift() || {}
			, opts = args.pop() || {}
			, result;
	
		// No options object -- if there are optiony names
		// in the data, copy them to options
		if (arguments.length == 3) {
			cpOptsInData(data, opts);
		}
		opts.filename = path;
	
		try {
			result = handleCache(opts)(data);
		}
		catch(err) {
			return cb(err);
		}

		return cb(null, result);
	};
	
	/**
	 * Clear intermediate JavaScript cache. Calls {@link Cache#reset}.
	 * @public
	 */
	
	exports.clearCache = function () {
		exports.cache.reset();
	};
	
	function Template(text, opts) {
		opts = opts || {};
		var options = {};
		this.templateText = text;
		this.mode = null;
		this.truncate = false;
		this.currentLine = 1;
		this.source = '';
		options.client = opts.client || false;
		options.escapeFunction = opts.escape || utils.escapeXML;
		options.compileDebug = opts.compileDebug !== false;
		options.debug = !!opts.debug;
		options.filename = opts.filename;
		options.delimiter = opts.delimiter || exports.delimiter || _DEFAULT_DELIMITER;
		options._with = typeof opts._with != 'undefined' ? opts._with : true;
		options.cache = true;
		options.rmWhitespace = opts.rmWhitespace;
		this.opts = options;

		this.regex = this.createRegex();
	}
	
	Template.modes = {
		EVAL: 'eval'
	, ESCAPED: 'escaped'
	, RAW: 'raw'
	, COMMENT: 'comment'
	, LITERAL: 'literal'
	};
	
	Template.prototype = {
		createRegex: function () {
			var str = _REGEX_STRING
				, delim = utils.escapeRegExpChars(this.opts.delimiter);
			str = str.replace(/%/g, delim);
			return new RegExp(str);
		}
	
	, compile: function () {
			var src
				, fn
				, opts = this.opts
				, escape = opts.escapeFunction;

			if (opts.rmWhitespace) {
				// Have to use two separate replace here as `^` and `$` operators don't
				// work well with `\r`.
				this.templateText =
					this.templateText.replace(/\r/g, '').replace(/^\s+|\s+$/gm, '');
			}
			if (!this.source) {
				this.generateSource();
				var prepended = '  var __output = [];' + '\n';
				if (opts._with !== false) {
					prepended +=  '  with (' + exports.localsName + ' || {}) {' + '\n';
				}
				this.source  = prepended + this.source;
				if (opts._with !== false) {
					this.source += '  }' + '\n';
				}
				this.source += '  return __output.join("");' + '\n';
			}
	
			if (opts.compileDebug) {
				src = 'var __line = 1' + '\n'
						+ '  , __lines = ' + JSON.stringify(this.templateText) + '\n'
						+ '  , __filename = ' + (opts.filename ?
									JSON.stringify(opts.filename) : 'undefined') + ';' + '\n'
						+ 'try {' + '\n'
						+ this.source
						+ '} catch (e) {' + '\n'
						+ '  rethrow(e, __lines, __filename, __line);' + '\n'
						+ '}' + '\n';
			}
			else {
				src = this.source;
			}
	
			if (opts.debug) {
				console.log(src);
			}
	
			if (opts.client) {
				src = 'escape = escape || ' + escape.toString() + ';' + '\n' + src;
				if (opts.compileDebug) {
					src = 'rethrow = rethrow || ' + rethrow.toString() + ';' + '\n' + src;
				}
			}
	
			try {
				fn = new Function(exports.localsName + ', escape, include, rethrow', src);
			}
			catch(e) {
				// istanbul ignore else
				if (e instanceof SyntaxError) {
					if (opts.filename) {
						e.message += ' in ' + opts.filename;
					}
					e.message += ' while compiling ejs';
				}
				throw e;
			}
	
			if (opts.client) {
				return fn;
			}
	
			// Return a callable function which will execute the function
			// created by the source-code, with the passed data as locals
			return function (data) {
				var include = function (path, includeData) {
					var d = utils.shallowCopy({}, data);
					if (includeData) {
						d = utils.shallowCopy(d, includeData);
					}
					return includeFile(path, opts)(d);
				};
				return fn(data || {}, escape, include, rethrow);
			};
	
		}
	
	, generateSource: function () {
			var self = this
				, matches = this.parseTemplateText()
				, d = this.opts.delimiter;

			if (matches && matches.length) {
				matches.forEach(function (line, index) {
					var closing
						, include
						, includeOpts
						, includeSrc;
					// If this is an opening tag, check for closing tags
					// FIXME: May end up with some false positives here
					// Better to store modes as k/v with '<' + delimiter as key
					// Then this can simply check against the map
					if ( line.indexOf('<' + d) === 0        // If it is a tag
						&& line.indexOf('<' + d + d) !== 0) { // and is not escaped
						closing = matches[index + 2];
						if (!(closing == d + '>' || closing == '-' + d + '>')) {
							throw new Error('Could not find matching close tag for "' + line + '".');
						}
					}
					// HACK: backward-compat `include` preprocessor directives
					if ((include = line.match(/^\s*include\s+(\S+)/))) {
						includeOpts = utils.shallowCopy({}, self.opts);
						includeOpts.filename = include[1];
						includeSrc = includeSource(include[1], includeOpts);
						includeSrc = '    ; (function(){' + '\n' + includeSrc + '    ; })()' + '\n';
						self.source += includeSrc;
					}
					else {
						self.scanLine(line);
					}
				});
			}
	
		}
	
	, parseTemplateText: function () {
			var str = this.templateText
				, pat = this.regex
				, result = pat.exec(str)
				, arr = []
				, firstPos
				, lastPos;

			while (result) {
				firstPos = result.index;
				lastPos = pat.lastIndex;
	
				if (firstPos !== 0) {
					arr.push(str.substring(0, firstPos));
					str = str.slice(firstPos);
				}
	
				arr.push(result[0]);
				str = str.slice(result[0].length);
				result = pat.exec(str);
			}
	
			if (str) {
				arr.push(str);
			}
	
			return arr;
		}
	
	, scanLine: function (line) {
			var self = this
				, d = this.opts.delimiter
				, newLineCount = 0;
	
			function _addOutput() {
				if (self.truncate) {
					line = line.replace('\n', '');
					self.truncate = false;
				}
				else if (self.opts.rmWhitespace) {
					// Gotta me more careful here.
					// .replace(/^(\s*)\n/, '$1') might be more appropriate here but as
					// rmWhitespace already removes trailing spaces anyway so meh.
					line = line.replace(/^\n/, '');
				}
				if (!line) {
					return;
				}
	
				// Preserve literal slashes
				line = line.replace(/\\/g, '\\\\');
	
				// Convert linebreaks
				line = line.replace(/\n/g, '\\n');
				line = line.replace(/\r/g, '\\r');
	
				// Escape double-quotes
				// - this will be the delimiter during execution
				line = line.replace(/"/g, '\\"');
				self.source += '    ; __output.push("' + line + '")' + '\n';
			}
	
			newLineCount = (line.split('\n').length - 1);
	
			switch (line) {
				case '<' + d:
					this.mode = Template.modes.EVAL;
					break;
				case '<' + d + '=':
					this.mode = Template.modes.ESCAPED;
					break;
				case '<' + d + '-':
					this.mode = Template.modes.RAW;
					break;
				case '<' + d + '#':
					this.mode = Template.modes.COMMENT;
					break;
				case '<' + d + d:
					this.mode = Template.modes.LITERAL;
					this.source += '    ; __output.push("' + line.replace('<' + d + d, '<' + d) + '")' + '\n';
					break;
				case d + '>':
				case '-' + d + '>':
					if (this.mode == Template.modes.LITERAL) {
						_addOutput();
					}
	
					this.mode = null;
					this.truncate = line.indexOf('-') === 0;
					break;
				default:
					// In script mode, depends on type of tag
					if (this.mode) {
						// If '//' is found without a line break, add a line break.
						switch (this.mode) {
							case Template.modes.EVAL:
							case Template.modes.ESCAPED:
							case Template.modes.RAW:
								if (line.lastIndexOf('//') > line.lastIndexOf('\n')) {
									line += '\n';
								}
						}
						switch (this.mode) {
							// Just executing code
							case Template.modes.EVAL:
								this.source += '    ; ' + line + '\n';
								break;
							// Exec, esc, and output
							case Template.modes.ESCAPED:
								this.source += '    ; __output.push(escape(' +
									line.replace(_TRAILING_SEMCOL, '').trim() + '))' + '\n';
								break;
							// Exec and output
							case Template.modes.RAW:
								this.source += '    ; __output.push(' +
									line.replace(_TRAILING_SEMCOL, '').trim() + ')' + '\n';
								break;
							case Template.modes.COMMENT:
								// Do nothing
								break;
							// Literal <%% mode, append as raw output
							case Template.modes.LITERAL:
								_addOutput();
								break;
						}
					}
					// In string mode, just add the output
					else {
						_addOutput();
					}
			}
	
			if (self.opts.compileDebug && newLineCount) {
				this.currentLine += newLineCount;
				this.source += '    ; __line = ' + this.currentLine + '\n';
			}
		}
	};
	
	/**
	 * Express.js support.
	 *
	 * This is an alias for {@link module:ejs.renderFile}, in order to support
	 * Express.js out-of-the-box.
	 *
	 * @func
	 */
	
	exports.__express = exports.renderFile;
	
	exports.load = function(){
		var args = slice.call(arguments);
		var id = document.getElementById(args[0]);
		var fn = function(err, str){
			if ( err ){
				console.error(err.message);
			}else{
				id.innerHTML = str;
			}
		}
		args.push(fn);
		exports.renderFile.apply(exports, args)
	}
	
	exports.listens = [];
	
	exports.ready = function(fn){
		exports.listens.push(fn);
	}
	
	exports.autoReady = function(){
		exports.listens.forEach(function(listen){
			listen();
		});
	}
	
	series.nodebone = exports;
	series.domReady(domReady);
	
	function getAttr(key){
		return this.getAttribute(key);
	}
	
	function setAttr(key, value){
		return this.setAttribute(key, value);
	}
	
	function nodeCache(node, data, opts, from){
		this.data = data;
		this.opts = opts;
		this.from = from;
		this.node = node;
		this.id = getAttr.call(node, 'id');
	}
	
	nodeCache.prototype.refresh = function(data){
		var that = this, po;
		if ( !data ){
			if ( this.from ){
				po = new Promise(function(resolve, reject){
					ajax.getJSON(that.from, function(d){
						that.data = utils.shallowCopy(that.data, d);
						resolve();
					});
				});
			}else{
				po = Promise.resolve();
			}
		}
		else if ( series.isString(data) ){
			po = new Promise(function(resolve, reject){
				ajax.getJSON(data, function(d){
					that.data = utils.shallowCopy(that.data, d);
					resolve();
				});
			});
		}
		else{
			that.data = utils.shallowCopy(that.data, data);
			po = Promise.resolve();
		}
		po.then(function(){
			fn = exports.compile(exports.templatecache[that.id], that.opts);
			exports.cache.set(that.id, fn);
			that.node.innerHTML = fn(that.data);
		});
	}
	
	nodeCache.prototype.getTempalte = function(){
		return exports.templatecache[this.id];
	}
	
	nodeCache.prototype.setTempalte = function(val){
		exports.templatecache[this.id] = val;
	}
	
	exports.nodeBind = function(data, opts, from){
		var caches = new nodeCache(this, data, opts, from);
		var that = this;
		['refresh'].forEach(function(m){
			if ( caches[m] ){
				that[m] = caches[m].bind(caches);
			}else{
				console.error('no such method on node');
			}
		});
		Object.defineProperty(this, "scope", {
			get: function () 		{ return caches.data; },
			set: function (val) { caches.refresh(val); }
		});
		
		Object.defineProperty(this, "template", {
			get: function () 		{ return caches.getTempalte(); },
			set: function (val) { caches.setTempalte(val); }
		});
	}
	
	function domReady(){
		var nodes;
		try{
			nodes = slice.call(document.getElementsByName('nodebone'));
		}catch(e){
			nodes = [];
			var el = document.getElementsByName('nodebone');
			for ( var i = 0 ; i < el.length; i++ ){
				nodes.push(el[i])
			}
		}
		var pools = [];
		nodes.forEach(function(node){
			var id = getAttr.call(node, 'id');
			var required = getAttr.call(node, 'nodebone-mode');
			var url = getAttr.call(node, 'nodebone-ajax');
			if ( id && required === 'required' && url ){
				pools.push(new Promise(function(resolve, reject){
					ajax.getJSON(url, function(data){
						var _opts = {delimiter: '$'};
						exports.load(id, data, _opts);
						exports.nodeBind.call(node, data, _opts, url);
						resolve();
					});
				}));
			}
		});

		Promise.all(pools).then(function(){
			typeof exports.ready === 'function' && exports.autoReady();
		});
	}
	
}).call(this);