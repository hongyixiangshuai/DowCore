import { CoreTemplatePage } from './app.po';

describe('Core App', function() {
  let page: CoreTemplatePage;

  beforeEach(() => {
    page = new CoreTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
