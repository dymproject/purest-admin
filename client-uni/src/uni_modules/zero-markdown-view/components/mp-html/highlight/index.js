/**
 * @fileoverview highlight 插件
 * Include prismjs (https://prismjs.com)
 */
import prism from './prism.min'
import config from './config'
import Parser from '../parser'

function Highlight (vm) {
  this.vm = vm
}

Highlight.prototype.onParse = function (node, vm) {
  if (node.name === 'pre') {
    if (vm.options.editable) {
      node.attrs.class = (node.attrs.class || '') + ' hl-pre'
      return
    }
    let i
    for (i = node.children.length; i--;) {
      if (node.children[i].name === 'code') break
    }
    if (i === -1) return
    const code = node.children[i]
    let className = code.attrs.class + ' ' + node.attrs.class
    i = className.indexOf('language-')
    if (i === -1) {
      i = className.indexOf('lang-')
      if (i === -1) {
        className = 'language-text'
        i = 9
      } else {
        i += 5
      }
    } else {
      i += 9
    }
    let j
    for (j = i; j < className.length; j++) {
      if (className[j] === ' ') break
    }
    const lang = className.substring(i, j)
    if (code.children.length) {
      const text = this.vm.getText(code.children).replace(/&amp;/g, '&')
      if (!text) return
      if (node.c) {
        node.c = undefined
      }
      if (prism.languages[lang]) {
        code.children = (new Parser(this.vm).parse(
          // 加一层 pre 保留空白符
          '<pre>' + prism.highlight(text, prism.languages[lang], lang).replace(/token /g, 'hl-') + '</pre>'))[0].children
      }
      node.attrs.class = 'hl-pre'
      code.attrs.class = 'hl-code'
	  code.attrs.style ='display:block;overflow: auto;'
      if (config.showLanguageName) {
        node.children.push({
          name: 'div',
          attrs: {
            class: 'hl-language',
            style: 'user-select:none;position:absolute;top:0;right:2px;font-size:10px;'
          },
          children: [{
            type: 'text',
            text: lang
          }]
        })
      }
      if (config.copyByClickCode) {
        node.attrs.style += (node.attrs.style || '') + ';user-select:none;'
        node.attrs['data-content'] = text
		node.children.push({
		  name: 'div',
		  attrs: {
		    class: 'hl-copy',
		    style: 'user-select:none;position:absolute;top:0;right:3px;font-size:10px;'
		  },
		  // children: [{
		  //   type: 'text',
		  //   text: '复制'
		  // }]
		})
        vm.expose()
		// console.log('vm',node,vm)
      }
      if (config.showLineNumber) {
        const line = text.split('\n').length; const children = []
        for (let k = line; k--;) {
          children.push({
            name: 'span',
            attrs: {
              class: 'span'
            }
          })
        }
        node.children.push({
          name: 'span',
          attrs: {
            class: 'line-numbers-rows'
          },
          children
        })
      }
    }
  }
}

export default Highlight
