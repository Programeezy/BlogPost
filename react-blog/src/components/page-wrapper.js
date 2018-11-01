import _ from 'lodash'
import DocumentTitle from 'react-document-title'
import styled from 'styled-components'

import React from 'react'
import { HotKeys } from 'react-hotkeys'
import { connectTo } from '../utils/generic'
import { enterPage, exitPage } from '../actions/generic'

import CircularProgress from '@material-ui/core/CircularProgress';

const Loading = styled.div`
  height: 100vh;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
`

class PageWrapper extends React.Component {
  render() {
    const {
      children,
      keyMap,
      handlers,
      stateReceived,
      page,
      documentTitle = 'Programeezy\'s Blog',
      style
    } = this.props
    this.page = page
    return stateReceived ? (
      <DocumentTitle title={documentTitle}>
        {_.isEmpty(keyMap) ? (
          <div style={style}>
            {children}
          </div>
        ) : (
          <HotKeys
            style={style}
            keyMap={keyMap}
            handlers={handlers}
            focused
          >
            {children}
          </HotKeys>
        )}
      </DocumentTitle>
    ) : (
      <Loading>
        <CircularProgress/>
      </Loading>
    )
  }

  componentDidMount() {
    this.props.enterPage()
  }

  componentWillUnmount() {
    this.props.exitPage(this.page)
  }
}

export default connectTo(
  state => ({
    page: state.navigation.page,
    stateReceived: state.cache.stateReceived[state.navigation.page]
  }),
  { enterPage, exitPage },
  PageWrapper
)
