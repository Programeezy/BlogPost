export const BACKEND = `https://localhost:44362/api/`

const AUTH = `${BACKEND}auth/`
export const LOGIN = `${AUTH}login`
export const REGISTER = `${AUTH}register`

export const STORIES = `${BACKEND}articles/`
export const CREATE_STORY = STORIES
export const DELETE_STORY = STORIES
export const UPDATE_STORY = storyId => `${STORIES}${storyId}`
export const PUBLISH_STORY = storyId => `${STORIES}${storyId}/publish`

export const USER_STORIES = userId => `${STORIES}user/${userId}`
export const STORY_DETAIL = storyId => `${STORIES}${storyId}`
export const DRAFTS = `${STORIES}drafts`

