!function(e){var t={};function r(n){if(t[n])return t[n].exports;var o=t[n]={i:n,l:!1,exports:{}};return e[n].call(o.exports,o,o.exports,r),o.l=!0,o.exports}r.m=e,r.c=t,r.d=function(e,t,n){r.o(e,t)||Object.defineProperty(e,t,{configurable:!1,enumerable:!0,get:n})},r.n=function(e){var t=e&&e.__esModule?function(){return e.default}:function(){return e};return r.d(t,"a",t),t},r.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},r.p="",r(r.s=10)}([function(e,t){e.exports=function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var r in e)if(Object.prototype.hasOwnProperty.call(e,r)){var n=Object.defineProperty&&Object.getOwnPropertyDescriptor?Object.getOwnPropertyDescriptor(e,r):{};n.get||n.set?Object.defineProperty(t,r,n):t[r]=e[r]}return t.default=e,t}},function(e,t){e.exports=function(e){return e&&e.__esModule?e:{default:e}}},function(e,t){function r(){return e.exports=r=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var r=arguments[t];for(var n in r)Object.prototype.hasOwnProperty.call(r,n)&&(e[n]=r[n])}return e},r.apply(this,arguments)}e.exports=r},function(e,t){e.exports=function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}},function(e,t){function r(e,t){for(var r=0;r<t.length;r++){var n=t[r];n.enumerable=n.enumerable||!1,n.configurable=!0,"value"in n&&(n.writable=!0),Object.defineProperty(e,n.key,n)}}e.exports=function(e,t,n){return t&&r(e.prototype,t),n&&r(e,n),e}},function(e,t,r){var n=r(12),o=r(13);e.exports=function(e,t){return!t||"object"!==n(t)&&"function"!=typeof t?o(e):t}},function(e,t){function r(t){return e.exports=r=Object.setPrototypeOf?Object.getPrototypeOf:function(e){return e.__proto__||Object.getPrototypeOf(e)},r(t)}e.exports=r},function(e,t,r){var n=r(14);e.exports=function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function");e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,writable:!0,configurable:!0}}),t&&n(e,t)}},function(e,t){e.exports=React},function(e,t){e.exports=PropTypes},function(e,t,r){"use strict";window.wsb=window.wsb||{},window.wsb.WrappedAbsLink=window.wsb.WrappedAbsLink||r(11)},function(e,t,r){"use strict";var n=r(0),o=r(1);Object.defineProperty(t,"__esModule",{value:!0}),t.default=void 0;var u=o(r(2)),i=o(r(3)),f=o(r(4)),c=o(r(5)),s=o(r(6)),p=o(r(7)),l=n(r(8)),a=o(r(9)),d=o(r(15)),y=o(r(16)),b=function(e){function t(){return(0,i.default)(this,t),(0,c.default)(this,(0,s.default)(t).apply(this,arguments))}return(0,p.default)(t,e),(0,f.default)(t,[{key:"render",value:function(){var e=this.props.href,t=this.props.isActive,r="undefined"!=typeof window&&(0,d.default)(window.location.href)||{};return r.r&&(t=r.r===encodeURIComponent(e)),l.default.createElement(y.default,(0,u.default)({},this.props,{isActive:t}))}}]),t}(l.Component);b.propTypes={href:a.default.string,isActive:a.default.bool};var v=b;t.default=v,e.exports=t.default},function(e,t){function r(e){return(r="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(e){return typeof e}:function(e){return e&&"function"==typeof Symbol&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":typeof e})(e)}function n(t){return"function"==typeof Symbol&&"symbol"===r(Symbol.iterator)?e.exports=n=function(e){return r(e)}:e.exports=n=function(e){return e&&"function"==typeof Symbol&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":r(e)},n(t)}e.exports=n},function(e,t){e.exports=function(e){if(void 0===e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return e}},function(e,t){function r(t,n){return e.exports=r=Object.setPrototypeOf||function(e,t){return e.__proto__=t,e},r(t,n)}e.exports=r},function(e,t,r){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),t.default=function(e){var t,r={},n=/(\?|&)([^=]+)=([^&]+)/g;do{(t=n.exec(e))&&(r[t[2]]=t[3])}while(t);return r},e.exports=t.default},function(e,t,r){"use strict";var n=r(0),o=r(1);Object.defineProperty(t,"__esModule",{value:!0}),t.default=void 0;var u=o(r(2)),i=o(r(17)),f=o(r(3)),c=o(r(4)),s=o(r(5)),p=o(r(6)),l=o(r(7)),a=n(r(8)),d=o(r(9)),y=r(19),b={"/idx/wrapper":!0},v=function(e){function t(){return(0,f.default)(this,t),(0,s.default)(this,(0,p.default)(t).apply(this,arguments))}return(0,l.default)(t,e),(0,c.default)(t,[{key:"render",value:function(){var e,t=y.UX2.Element.Link,r=this.props,n=r.renderMode,o=r.pageRoute,f=r.isActive,c=r.isNested,s=(0,i.default)(r,["renderMode","pageRoute","isActive","isNested"]),p=n===y.constants.renderModes.PUBLISH&&b[o];return e=c?f?t.NestedActive:t.Nested:f?t.Active:t,a.default.createElement(e,(0,u.default)({convertToAbsolute:p},s))}}]),t}(a.Component);v.propTypes={renderMode:d.default.string,pageRoute:d.default.string,isActive:d.default.bool,isNested:d.default.bool};var O=v;t.default=O,e.exports=t.default},function(e,t,r){var n=r(18);e.exports=function(e,t){if(null==e)return{};var r,o,u=n(e,t);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);for(o=0;o<i.length;o++)r=i[o],t.indexOf(r)>=0||Object.prototype.propertyIsEnumerable.call(e,r)&&(u[r]=e[r])}return u}},function(e,t){e.exports=function(e,t){if(null==e)return{};var r,n,o={},u=Object.keys(e);for(n=0;n<u.length;n++)r=u[n],t.indexOf(r)>=0||(o[r]=e[r]);return o}},function(e,t){e.exports=Core}]);