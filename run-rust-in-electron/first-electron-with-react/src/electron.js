/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = "./src/electron.ts");
/******/ })
/************************************************************************/
/******/ ({

/***/ "./src/electron.ts":
/*!*************************!*\
  !*** ./src/electron.ts ***!
  \*************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

eval("// const { app, BrowserWindow } = require('electron')\n// // Keep a global reference of the window object, if you don't, the window will\n// // be closed automatically when the JavaScript object is garbage collected.\n// let win\n// function createWindow () {\n//   // Create the browser window.\n//   win = new BrowserWindow({\n//     width: 800,\n//     height: 600,\n//     webPreferences: {\n//       nodeIntegration: true\n//     }\n//   })\n//   // and load the index.html of the app.\n//   win.loadFile('index.html')\n//   // Open the DevTools.\n//   win.webContents.openDevTools()\n//   // Emitted when the window is closed.\n//   win.on('closed', () => {\n//     // Dereference the window object, usually you would store windows\n//     // in an array if your app supports multi windows, this is the time\n//     // when you should delete the corresponding element.\n//     win = null\n//   })\n// }\n// // This method will be called when Electron has finished\n// // initialization and is ready to create browser windows.\n// // Some APIs can only be used after this event occurs.\n// app.on('ready', createWindow)\n// // Quit when all windows are closed.\n// app.on('window-all-closed', () => {\n//   // On macOS it is common for applications and their menu bar\n//   // to stay active until the user quits explicitly with Cmd + Q\n//   if (process.platform !== 'darwin') {\n//     app.quit()\n//   }\n// })\n// app.on('activate', () => {\n//   // On macOS it's common to re-create a window in the app when the\n//   // dock icon is clicked and there are no other windows open.\n//   if (win === null) {\n//     createWindow()\n//   }\n// })\n// // In this file you can include the rest of your app's specific main process\n// // code. You can also put them in separate files and require them here.\n// src/electron.js\nvar _a = __webpack_require__(/*! electron */ \"electron\"), app = _a.app, BrowserWindow = _a.BrowserWindow;\nfunction createWindow() {\n    // Create the browser window.\n    var win = new BrowserWindow({\n        width: 800,\n        height: 600,\n        webPreferences: {\n            nodeIntegration: true\n        }\n    });\n    // and load the index.html of the app.\n    win.loadFile('index.html');\n}\napp.on('ready', createWindow);\n\n\n//# sourceURL=webpack:///./src/electron.ts?");

/***/ }),

/***/ "electron":
/*!***************************!*\
  !*** external "electron" ***!
  \***************************/
/*! no static exports found */
/***/ (function(module, exports) {

eval("module.exports = require(\"electron\");\n\n//# sourceURL=webpack:///external_%22electron%22?");

/***/ })

/******/ });